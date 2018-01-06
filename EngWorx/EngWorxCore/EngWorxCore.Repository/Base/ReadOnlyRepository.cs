using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Reflection;
using log4net;
using RepositoryInterface;

namespace Repository
{
    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        protected static ILog log = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        protected DbContext db = null;

        public ReadOnlyRepository(DbContext entities)
        {
            db = entities;

            db.Database.CommandTimeout = 180;

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<T> GetDbSet()
        {
            return db.Set<T>();
        }

        public T GetByPrimaryKey(params object[] keyValues)
        {
            return GetDbSet().Find(keyValues);
        }

        public IList<T> GetAll()
        {
            return (from c in GetDbSet()
                    select c).ToList();
        }

        private bool isValidFilteringField(PropertyInfo p, object value)
        {
            bool result = false;
            if (value == null)
            {
                result = false;
            }
            else if (p.PropertyType == typeof(Byte) || p.PropertyType == typeof(Byte?))
            {
                Byte val = Byte.Parse(value.ToString());
                if (val != 0)
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(Int16) || p.PropertyType == typeof(Int16?))
            {
                Int16 val = Int16.Parse(value.ToString());
                if (val != 0)
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(Int32) || p.PropertyType == typeof(Int32?))
            {
                Int32 val = Int32.Parse(value.ToString());
                if (val != 0)
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(Int64) || p.PropertyType == typeof(Int64?))
            {
                Int64 val = Int64.Parse(value.ToString());
                if (val != 0)
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(Decimal) || p.PropertyType == typeof(Decimal?))
            {
                Decimal val = Decimal.Parse(value.ToString());
                if (val != 0)
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
            {
                DateTime val = DateTime.Parse(value.ToString());
                if (!DateTime.MinValue.Equals(val))
                {
                    result = true;
                }
            }
            else if (p.PropertyType == typeof(String))
            {
                result = true;
            }
            else if (p.PropertyType.IsPrimitive)
            {
                throw new Exception("Il tipo di dato del campo '" + p.Name + "' su cui effettuare il filtro è sconosciuto.");
            }
            else
            {
                return false;
            }
            return result;
        }

        private bool isValidOrderingField(PropertyInfo p)
        {
            bool result = false;
            if (!p.PropertyType.IsInterface)
            {
                result = true;
            }
            return result;
        }

        public IList<T> GetFiltered(T parameters, string sort = "", string sortDirection = "")
        {
            int r;
            return GetFiltered(parameters, sort, sortDirection, 0, 0, out r);
        }

        public IList<T> GetFiltered(T parameters, int pageNumber, int pageSize, out int totalrows)
        {
            return GetFiltered(parameters, "", "", pageNumber, pageSize, out totalrows);
        }

        public IList<T> GetFiltered(T parameters, string sort, string sortDirection, int pageNumber, int pageSize, out int totalrows)
        {
            return GetFiltered(parameters, sort, sortDirection, pageNumber, pageSize, new List<string>(), out totalrows);
        }

        public IList<T> GetFiltered(T parameters, string sort, string sortDirection, int pageNumber, int pageSize, List<string> lstInclude, out int totalrows)
        {
            /****************************/
            // CREAZIONE QUERY DI BASE
            /****************************/
            var lista = from e in GetDbSet()
                        select e;

            foreach (string s in lstInclude)
            {
                lista = lista.Include(s);
            }

            //log.Debug(((DbQuery<T>)lista).ToString());

            /****************************/
            // FILTRI
            /****************************/
            if (parameters == null)
            {
                throw new Exception("Il parametro di filtro fornito è null");
            }
            Type t = parameters.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                object value = p.GetValue(parameters, null);
                if (isValidFilteringField(p, value))
                {
                    ParameterExpression paramExpr = Expression.Parameter(t, "x");
                    Expression leftExpr = Expression.Property(paramExpr, p);
                    ConstantExpression rightExpr = null;
                    if (p.PropertyType == typeof(String))
                    {
                        rightExpr = Expression.Constant(value.ToString().Replace("*", "").Replace("!", ""), value.GetType());
                    }
                    else
                    {
                        rightExpr = Expression.Constant(value, value.GetType());
                    }

                    Expression convertedRightExpr = null;
                    if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // in questo caso il tipo è Nullable
                        convertedRightExpr = Expression.Convert(rightExpr, p.PropertyType);
                    }
                    else
                    {
                        // in questo caso il tipo NON è Nullable
                        convertedRightExpr = rightExpr;
                    }

                    //BinaryExpression binExpr = Expression.Equal(leftExpr, convertedRightExpr);
                    Expression binExpr = null;
                    //*******************************************************//
                    // Utilizzo dei wildcards se e solo se il tipo è stringa //
                    //*******************************************************//
                    Expression<Func<T, bool>> lambdaExpr = null;
                    if (p.PropertyType == typeof(String))
                    {
                        // controllare wildcards
                        bool bNot = false;
                        foreach (string val in value.ToString().Split('&'))
                        {
                            bNot = false;
                            binExpr = null;
                            int bitWildcards = 0;
                            string v = val.Trim();
                            rightExpr = Expression.Constant(v.ToString().Replace("*", "").Replace("!", ""), v.GetType());
                            convertedRightExpr = rightExpr;

                            if ("!".Equals(v.ToString().Substring(0, 1)))
                            {
                                bNot = true; v = v.ToString().Replace("!", "");
                            }
                            if ("*".Equals(v.ToString().Substring(0, 1)))
                            {
                                bitWildcards += 1;
                            }
                            if ("*".Equals(v.ToString().Substring(v.ToString().Length - 1, 1)))
                            {
                                bitWildcards += 2;
                            }
                            switch (bitWildcards)
                            {
                                case 1:
                                    binExpr = Expression.Call(leftExpr, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), convertedRightExpr);
                                    break;
                                case 2:
                                    binExpr = Expression.Call(leftExpr, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), convertedRightExpr);
                                    break;
                                case 3:
                                    binExpr = Expression.Call(leftExpr, typeof(string).GetMethod("Contains", new[] { typeof(string) }), convertedRightExpr);

                                    break;
                                default:
                                    binExpr = Expression.Equal(leftExpr, convertedRightExpr);
                                    break;
                            }

                            if (bNot)
                            {
                                binExpr = Expression.Not(binExpr);
                            }
                            lambdaExpr = Expression.Lambda<Func<T, bool>>(binExpr, new ParameterExpression[] { paramExpr });
                            lista = lista.Where(lambdaExpr);
                        }
                    }
                    else
                    {
                        binExpr = Expression.Equal(leftExpr, convertedRightExpr);
                        lambdaExpr = Expression.Lambda<Func<T, bool>>(binExpr, new ParameterExpression[] { paramExpr });
                        lista = lista.Where(lambdaExpr);
                    }
                    // lambdaExpr = Expression.Lambda<Func<T, bool>>(binExpr, new ParameterExpression[] { paramExpr });
                    //lista = lista.Where(lambdaExpr);
                }
            }
            totalrows = (pageNumber > 0 && pageSize > 0) ? lista.Count() : 0;
            //log.Debug(((DbQuery<T>)lista).ToString());

            /*****************/
            // ORDINAMENTO
            /*****************/
            if (String.Empty.Equals(sort))
            {
                foreach (PropertyInfo p in properties)
                {
                    if (isValidOrderingField(p))
                    {
                        sort = p.Name;
                        break;
                    }
                }
            }
            if (String.Empty.Equals(sortDirection))
            {
                sortDirection = "ASC";
            }
            if (!"ASC".Equals(sortDirection) && !"DESC".Equals(sortDirection))
            {
                throw new Exception("Valore del parametro sortDirection errato");
            }

            PropertyInfo orderingProperty = t.GetProperty(sort);
            if (orderingProperty == null)
            {
                throw new Exception("Non esiste un parametro di nome '" + sort + "' su cui effettuare l'ordinamento");
            }
            ParameterExpression orderingParameter = Expression.Parameter(t, "x");
            Expression orderingExpression = Expression.Property(orderingParameter, orderingProperty);
            if (orderingProperty.PropertyType == typeof(Byte))
            {
                Expression<Func<T, Byte>> lambda = Expression.Lambda<Func<T, Byte>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Byte?))
            {
                Expression<Func<T, Byte?>> lambda = Expression.Lambda<Func<T, Byte?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int16))
            {
                Expression<Func<T, Int16>> lambda = Expression.Lambda<Func<T, Int16>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int16?))
            {
                Expression<Func<T, Int16?>> lambda = Expression.Lambda<Func<T, Int16?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int32))
            {
                Expression<Func<T, Int32>> lambda = Expression.Lambda<Func<T, Int32>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int32?))
            {
                Expression<Func<T, Int32?>> lambda = Expression.Lambda<Func<T, Int32?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int64))
            {
                Expression<Func<T, Int64>> lambda = Expression.Lambda<Func<T, Int64>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Int64?))
            {
                Expression<Func<T, Int64?>> lambda = Expression.Lambda<Func<T, Int64?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Decimal))
            {
                Expression<Func<T, Decimal>> lambda = Expression.Lambda<Func<T, Decimal>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Decimal?))
            {
                Expression<Func<T, Decimal?>> lambda = Expression.Lambda<Func<T, Decimal?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(DateTime))
            {
                Expression<Func<T, DateTime>> lambda = Expression.Lambda<Func<T, DateTime>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(DateTime?))
            {
                Expression<Func<T, DateTime?>> lambda = Expression.Lambda<Func<T, DateTime?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Guid))
            {
                Expression<Func<T, Guid>> lambda = Expression.Lambda<Func<T, Guid>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(Guid?))
            {
                Expression<Func<T, Guid?>> lambda = Expression.Lambda<Func<T, Guid?>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else if (orderingProperty.PropertyType == typeof(String))
            {
                Expression<Func<T, String>> lambda = Expression.Lambda<Func<T, String>>(orderingExpression, new ParameterExpression[] { orderingParameter });
                if ("ASC".Equals(sortDirection))
                {
                    lista = lista.OrderBy(lambda);
                }
                else
                {
                    lista = lista.OrderByDescending(lambda);
                }
            }
            else
            {
                throw new Exception("Il tipo di dato del campo '" + sort + "' su cui effettuare l'ordinamento è sconosciuto.");
            }

            /*****************/
            // PAGINAZIONE
            /*****************/
            //Controllo la validità dei valori di paginazione
            if (pageSize != 0 || pageNumber != 0)
            {
                if (pageSize > 0 && pageNumber > 0)
                {
                    //Controllo validità della pagina richiesta
                    int maxPageNumber = (totalrows / pageSize) + (((totalrows % pageSize) != 0) ? 1 : 0);
                    if (pageNumber > 1 && pageNumber > maxPageNumber)
                    {
                        // lancio un'eccezione nel caso ci sia almeno una riga di risultato e viene richiesta una pagina non valida
                        // In caso di totalRows == 0, non lancio l'eccezione
                        throw new Exception("La pagina richiesta (pag. " + pageNumber + ") non è valida perché ne esistono un massimo di " + maxPageNumber);
                    }
                    lista = lista.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                }
                else
                {
                    throw new Exception("I valori di pageSize e pageNumber non sono validi");
                }
            }
            log.Debug(((DbQuery<T>)lista).ToString());

            IList<T> returnList = lista.ToList();
            return returnList;
        }
    }
}
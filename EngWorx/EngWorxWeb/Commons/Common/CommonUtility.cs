using System;
using System.Reflection;
using System.Configuration;
using Crypt;
using System.Linq.Expressions;
using System.Linq;

namespace Common
{
    public class CommonUtility
    {
        private static string SECURITY_KEY = ConfigurationManager.AppSettings["SECURITY_KEY"];
        public static string EncryptStringAES(string stringToCrypt)
        {
            return CryptingUtility.doCrypt(stringToCrypt, SECURITY_KEY);
        }

        public static string DecryptStringAES(string sectoken)
        {
            return CryptingUtility.doDecrypt(sectoken, SECURITY_KEY);
        }

        public static string GenerateHashCode(object obj)
        {
            return HashingUtility.GetSHA1Hash(obj);
        }

        public static object GetFieldValue(object obj, string prop)
        {
            if (obj == null)
                return null;
            int index = prop.IndexOf('.');
            PropertyInfo p = null;
            if (index == -1)
            {
                p = obj.GetType().GetProperty(prop);
                if (p == null)
                {
                    return null;
                }
                return p.GetValue(obj, null);
            }
            p = obj.GetType().GetProperty(prop.Substring(0, index));
            if (p == null)
            {
                return null;
            }
            return GetFieldValue(p.GetValue(obj, null), prop.Substring(index + 1, prop.Length - index - 1));
        }

        public static IQueryable<T> AddFilterByWildCards<T>(IQueryable<T> lst, string prop, string param, bool trim = true)
        {
            if (!string.IsNullOrWhiteSpace(param))
            {
                param = param.Trim();

                // Not Equals
                bool notEquals = false;
                if (("!".Equals(param.Substring(0, 1))))
                {
                    notEquals = true;
                    param = param.Replace("!", "");
                }

                // WildCards
                int bitWildcards = 0;
                if (("*".Equals(param.Substring(0, 1))))
                {
                    bitWildcards += 1;
                }
                if (("*".Equals(param.Substring(param.Length - 1, 1))))
                {
                    bitWildcards += 2;
                }
                param = param.Replace("*", "");

                string[] propParts = prop.Split('.');
                ParameterExpression paramExp = Expression.Parameter(typeof(T), "x");
                MemberExpression propExp = Expression.Property(paramExp, propParts[0]);
                for (int i = 2; i <= propParts.Count(); i++)
                {
                    propExp = Expression.Property(propExp, propParts[i - 1]);
                }
                dynamic constExp = Expression.Constant(param);
                Expression callExp = default(Expression);
                if (trim)
                {
                    callExp = Expression.Call(propExp, typeof(string).GetMethod("Trim", new Type[] { }));
                }
                else
                {
                    callExp = propExp;
                }
                switch (bitWildcards)
                {
                    case 0:
                        // Equals
                        if (notEquals)
                        {
                            callExp = Expression.Not(Expression.Call(callExp, typeof(string).GetMethod("Equals", new Type[] { typeof(string) }), constExp));
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        else
                        {
                            callExp = Expression.Call(callExp, typeof(string).GetMethod("Equals", new Type[] { typeof(string) }), constExp);
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        break;
                    case 1:
                        // Ends With
                        if (notEquals)
                        {
                            callExp = Expression.Not(Expression.Call(callExp, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), constExp));
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        else
                        {
                            callExp = Expression.Call(callExp, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), constExp);
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        break;
                    case 2:
                        // Starts With
                        if (notEquals)
                        {
                            callExp = Expression.Not(Expression.Call(callExp, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), constExp));
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        else
                        {
                            callExp = Expression.Call(callExp, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), constExp);
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        break;
                    case 3:
                        // Like
                        if (notEquals)
                        {
                            callExp = Expression.Not(Expression.Call(callExp, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), constExp));
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        else
                        {
                            callExp = Expression.Call(callExp, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), constExp);
                            Expression<Func<T, bool>> funcExp = Expression.Lambda<Func<T, bool>>(callExp, paramExp);
                            funcExp.Compile();
                            lst = lst.Where(funcExp);
                        }
                        break;
                }
            }
            return lst;
        }
    }
}

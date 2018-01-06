using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RepositoryInterface
{
    public interface IReadOnlyRepository<T> where T : class
    {
        DbSet<T> GetDbSet();
        T GetByPrimaryKey(params object[] keyValues);
        IList<T> GetAll();

        IList<T> GetFiltered(T parameters, string sort = "", string sortDirection = "");
        IList<T> GetFiltered(T parameters, int pageNumber, int pageSize, out int totalrows);
        IList<T> GetFiltered(T parameters, string sort, string sortDirection, int pageNumber, int pageSize, out int totalrows);
        IList<T> GetFiltered(T parameters, string sort, string sortDirection, int pageNumber, int pageSize, List<String> lstInclude, out int totalrows);
    }
}
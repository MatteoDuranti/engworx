namespace RepositoryInterface
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {
        int Update(T entity);
        int InsertOnSubmit(T entity);
        int DeleteOnSubmit(T entity, bool deleteOnCascade = false);
        int SaveChanges();
    }
}

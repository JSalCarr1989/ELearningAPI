namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Guid Add(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetById(Guid id);
        void Update(T entity);
        void Delete(Guid id);

        void SaveChanges();
    }
}

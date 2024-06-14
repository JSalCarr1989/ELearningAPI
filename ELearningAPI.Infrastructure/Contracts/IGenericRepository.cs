namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        int Add(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        void Update(T entity);
        void Delete(int id);

        void SaveChanges();
    }
}

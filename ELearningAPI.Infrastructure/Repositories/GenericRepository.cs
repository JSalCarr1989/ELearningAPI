using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ELearningContext context;
        private readonly DbSet<T> dbSet;
        public GenericRepository(ELearningContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public Guid Add(T entity)
        {
            Guid idValue = Guid.Empty;
            try
            {

                dbSet.Add(entity);
                SaveChanges();

                var idProperty = entity.GetType().GetProperty("Id");

                idValue = (Guid)idProperty.GetValue(entity);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return idValue;
        }

        public async void Delete(Guid id)
        {

            var entity = await GetById(id);

            if(entity != null)
            {
                dbSet.Remove(entity);
                SaveChanges();
            }

            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {

            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }
    }
}

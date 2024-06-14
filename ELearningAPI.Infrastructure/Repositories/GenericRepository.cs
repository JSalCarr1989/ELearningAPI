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

        public int Add(T entity)
        {
            int idValue = 0;
            try
            {

                dbSet.Add(entity);
                SaveChanges();

                var idProperty = entity.GetType().GetProperty("Id");

                idValue = (int)idProperty.GetValue(entity);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return idValue;
        }

        public async void Delete(int id)
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

        public async Task<T> GetById(int id)
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

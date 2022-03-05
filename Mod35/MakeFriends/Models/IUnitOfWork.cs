namespace MakeFriends.Models;

public interface IUnitOfWork: IDisposable
{
  int SaveChanges(bool ensureAutoHistory = false);

  IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;

}
public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
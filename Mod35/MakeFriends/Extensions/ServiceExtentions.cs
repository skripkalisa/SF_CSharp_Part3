using MakeFriends.Data.Repository;
using MakeFriends.Data.UoW;

namespace MakeFriends.Extensions;

public static class ServiceExtentions
{
  public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    return services;
  }

  public static IServiceCollection AddCustomRepository<TEntity, TRepository>(this IServiceCollection services)
    where TEntity : class
    where TRepository : class, IRepository<TEntity>
  {
    services.AddScoped<IRepository<TEntity>, TRepository>();

    return services;
  }

}
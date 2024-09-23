using Ardalis.Specification;

namespace WalletRu.Application.Interfaces;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
}
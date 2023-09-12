using Ardalis.Specification;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : class
{

}
using Ardalis.Specification;
namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{

}
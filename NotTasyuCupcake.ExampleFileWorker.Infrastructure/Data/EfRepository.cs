using Ardalis.Specification.EntityFrameworkCore;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
namespace NotTasyuCupcake.ExampleFileWorker.Infrastructure.Data;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(DataContext dbContext) : base(dbContext)
    {
    }
}
using WebTemplate.Models;

namespace WebTemplate.IRepositories
{
    public interface ITestModelRepository : IGenericRepository<TestModel>
    {
        // here should be some specific methods which are not present in IGenericRepository
    }
}
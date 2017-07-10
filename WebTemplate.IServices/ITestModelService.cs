using WebTemplate.Models;

namespace WebTemplate.IServices
{
    public interface ITestModelService : IGenericService<TestModel>
    {
        // here should be some specific methods which are not present in IGenericService
    }
}
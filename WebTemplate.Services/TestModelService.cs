using WebTemplate.IRepositories;
using WebTemplate.IServices;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public class TestModelService : GenericService<TestModel>, ITestModelService
    {
        public TestModelService(ITestModelRepository repository) : base(repository)
        {
        }
    }
}

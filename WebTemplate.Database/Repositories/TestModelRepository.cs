using System.Data.Entity;
using WebTemplate.IRepositories;
using WebTemplate.Models;

namespace WebTemplate.Database.Repositories
{
    public class TestModelRepository : GenericRepository<TestModel>, ITestModelRepository
    {
        public TestModelRepository(DbContext context) : base(context)
        {
        }
    }
}
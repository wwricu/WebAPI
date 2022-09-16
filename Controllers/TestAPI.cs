using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAPI : ControllerBase
    {
        private static readonly TestEntity[] TestReturn = new TestEntity[3];
        [HttpGet]
        public TestEntity[] Get(int id, string name)
        {
            TestReturn[0] = new TestEntity();
            TestReturn[1] = new TestEntity();
            TestReturn[2] = new TestEntity();

            TestReturn[0].Age = id;
            TestReturn[1].Name = name;

            TestService.SugarTest();
            return TestReturn;
        }
        [HttpPost]
        public TestEntity Post([FromBody] TestEntity entity)
        {
            return entity;
        }
    }
}

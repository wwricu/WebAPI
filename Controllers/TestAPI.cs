using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using WebAPI.DAO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAPI : ControllerBase
    {
        private static readonly TestEntity[] TestReturn = new TestEntity[3];
        [HttpGet]
        public string Get()
        {
            try
            {
                var StaticDAO = new StaticDAO();
                StaticDAO.GenerateStaticData();
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        public TestEntity Post([FromBody] TestEntity entity)
        {
            return entity;
        }
    }
}

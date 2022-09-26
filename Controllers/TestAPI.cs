using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using WebAPI.DAO;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAPI : ControllerBase
    {
        private static readonly TestEntity[] TestReturn = new TestEntity[3];
        [HttpGet]
        public ResponseModel Get()
        {
            try
            {
                var mailService = MailService.GetInstance();
                mailService.SendMail("iswangwr@gmail.com",
                                    new string[]
                                    {
                                        "wang.weiran@icloud.com",
                                        "weiran.wang@uon.edu.au"
                                    },
                                    "Test Subjuect",
                                    "<h>Test Body</h>");
                return new SuccessResponseModel();
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        }
        [HttpPost]
        public TestEntity Post([FromBody] TestEntity entity)
        {
            return entity;
        }
    }
}

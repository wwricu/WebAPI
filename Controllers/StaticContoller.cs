using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StaticController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Course([FromQuery] CourseOffering Course)
        {
            try
            {
                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = StaticService.Query(Course),
                };
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
            return new FailureResponseModel();
        }
    }
}

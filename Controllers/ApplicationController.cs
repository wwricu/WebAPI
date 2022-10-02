using Microsoft.AspNetCore.Mvc;
using WebAPI.Entity;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApplicationController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Save([FromBody] Application application)
        {
            try
            {
                ApplicationService.Save(application);
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
        public ResponseModel Submit([FromBody] Application application)
        {
            try
            {
                return new SuccessResponseModel()
                {
                    obj = ApplicationService.Submit(application)
                };
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        }
        [HttpDelete]
        public void Delete([FromBody] Application application)
        {
            ApplicationService.Delete(application);
        }
    }
}

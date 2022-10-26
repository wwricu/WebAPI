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
        [HttpGet]
        public ResponseModel New()
        {
            try
            {
                return new SuccessResponseModel()
                {
                    obj = ApplicationService.Save(new Application())
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
        [HttpPost]
        public ResponseModel Update([FromBody] Application application)
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
        /* staff controller start */
        [HttpPost]
        public ResponseModel ChangeState([FromBody] Application application)
        {
            try
            {
                ApplicationService.ChangeState(application);
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
        public ResponseModel Approve([FromBody] Application application)
        {
            try
            {
                ApplicationService.Approve(application);
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
        public ResponseModel Assign([FromBody] Application application)
        {
            try
            {
                ApplicationService.Assign(application);
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
    }
}

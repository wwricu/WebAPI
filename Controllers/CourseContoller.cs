using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using WebAPI.Model;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        public ResponseModel Get([FromQuery] CourseOffering Course,
                                 [FromQuery] SysUser user,
                                 [FromQuery] Assessment assessment)
        {
            try
            {
                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = CourseService.Query(Course, user, assessment),
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
        public ResponseModel GetMultiple(CourseQueryModel model)
        {
            try
            {
                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = CourseService.QueryMultiple(model),
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
        public ResponseModel New([FromBody] CourseOffering course)
        {
            try
            {
                CourseService.Insert(course);
                return new SuccessResponseModel()
                {
                    Message = "Success",
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
        public ResponseModel Update([FromBody] CourseOffering course)
        {
            try
            {
                CourseService.Update(course);
                return new SuccessResponseModel()
                {
                    Message = "Success",
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
        public ResponseModel Delete([FromBody] CourseOffering course)
        {
            try
            {
                CourseService.Delete(course);
                return new SuccessResponseModel()
                {
                    Message = "Success",
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
    }
}

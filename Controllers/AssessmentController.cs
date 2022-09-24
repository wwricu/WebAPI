using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using WebAPI.Model;
using SqlSugar.Extensions;
using System.Diagnostics;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AssessmentController : ControllerBase
    {
        // New template
        [HttpPost]
        public ResponseModel New([FromBody] AssessmentTemplate template)
        {
            try
            {
                AssessmentService.Insert(template);
                return new SuccessResponseModel()
                {
                    Message = "insert a course template",
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
        [HttpGet]
        public ResponseModel Get([FromQuery] CourseOffering course,
                                 [FromQuery] SysUser student)
        {
            try
            {
                if (course != null)
                {
                    return new SuccessResponseModel()
                    {
                        obj = AssessmentService.QueryTemplates(course),
                        Message = "Got a course template",
                    };
                }
                if (student != null)
                {
                    return new SuccessResponseModel()
                    {
                        obj = AssessmentService.QueryInstance(student),
                        Message = "Got a course instance",
                    };
                }
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
        public ResponseModel Update(Assessment assessment)
        {
            try
            {
                AssessmentService.Update(assessment);
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

        [HttpDelete]
        public ResponseModel Delete([FromBody] AssessmentTemplate template)
        {
            try
            {
                AssessmentService.Delete(
                    new List<AssessmentTemplate>() { template });
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

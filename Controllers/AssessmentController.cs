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
        public ResponseModel New(AssessmentModel model)
        {
            try
            {
                if (model.CourseOffering != null)
                {
                    AssessmentService.Insert(model.CourseOffering,
                                             model.Assessment);
                    return new SuccessResponseModel()
                    {
                        Message = "insert a course template",
                    };
                }
                if (model.Student != null)
                {
                    AssessmentService.Insert(model.Student,
                                             model.Assessment);
                    return new SuccessResponseModel()
                    {
                        Message = "insert a course instance",
                    };
                }
                // AssessmentService.Insert(course, assessment);
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
        [HttpGet]
        public ResponseModel Get([FromQuery] AssessmentModel model)
        {
            try
            {
                if (model.CourseOffering != null)
                {

                    return new SuccessResponseModel()
                    {
                        obj = AssessmentService.QueryTemplates(model.CourseOffering),
                        Message = "Got a course template",
                    };
                }
                if (model.Student != null)
                {
                    return new SuccessResponseModel()
                    {
                        obj = AssessmentService.QueryInstance(model.Student),
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
        [HttpDelete]
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
        public ResponseModel Delete([FromBody] Assessment assessment)
        {
            try
            {
                AssessmentService.Delete(
                    new List<Assessment>() { assessment });
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

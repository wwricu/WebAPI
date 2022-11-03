using Microsoft.AspNetCore.Mvc;
using SqlSugar.DistributedSystem.Snowflake;
using System.Diagnostics;
using System.Security.Authentication;
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
                    obj = ApplicationService.Save(new Application()
                    {
                        StudentID = SessionService
                                        .GetSessionInfo(HttpContext.Session)
                                        .SysUserID,
                    }).ToString(),
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
        public ResponseModel Get([FromQuery] Application application,
                                 [FromQuery] SysUser user)
        {
            try
            {
                SessionService.isOwner(HttpContext.Session, application);
            }
            catch
            {
                var currentUser = SessionService.GetSessionInfo(HttpContext.Session);
                if (currentUser.Permission == 1)
                {
                    application.StudentID = currentUser.SysUserID;
                    user.Permission = 1;
                }
                else if (currentUser.Permission == 2)
                {
                    application.StaffID = currentUser.SysUserID;
                }
            }

            try
            {
                return new SuccessResponseModel()
                {
                    obj = ApplicationService.Query(application, user)
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
                SessionService.isOwner(HttpContext.Session, application);
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

                SessionService.isOwner(HttpContext.Session, application);
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
        public ResponseModel Delete([FromBody] Application application)
        {
            try
            {
                SessionService.isOwner(HttpContext.Session, application);
                ApplicationService.Delete(application);
                return new SuccessResponseModel();
            } catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        }
        /* staff controller start */
        [HttpPost]
        public ResponseModel ChangeState([FromBody] Application application)
        {

            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 2);
                SessionService.isOwner(HttpContext.Session, application);
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
                AuthenticationService.Authorization(HttpContext.Session, 2);
                SessionService.isOwner(HttpContext.Session, application);
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
                AuthenticationService.Authorization(HttpContext.Session, 2);
                SessionService.isOwner(HttpContext.Session, application);
                
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

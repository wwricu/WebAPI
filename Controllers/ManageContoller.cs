using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using System.Diagnostics;
using SqlSugar;
using WebAPI.Model;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ManageController : ControllerBase
    {
        [HttpPost]
        public ResponseModel AddUser([FromBody] SysUser NewUser)
        {

            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 3);

                ManageService.AddUser(NewUser);

                return new SuccessResponseModel()
                {
                    Message = "Success Added",
                    obj = new PublicInfoModel()
                    {
                        UserNumber = NewUser.UserNumber,
                        UserName = NewUser.UserName,
                        Email = NewUser.Email,
                        Permission = NewUser.Permission,
                        Academic = NewUser.Academic,
                    },
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
        public ResponseModel GetUsers([FromQuery] PrivateInfoModel PrivateInfo)
        {
            try
            {
                // AuthenticationService.Authorization(HttpContext.Session, 2);

                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = ManageService.QueryUsers(PrivateInfo),
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
        public ResponseModel GetCandidates([FromQuery] PrivateInfoModel PrivateInfo,
                                           [FromQuery] CourseOffering course)
        {
            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 2);

                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = ManageService.QueryCandidateUsers(PrivateInfo, course),
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
        public ResponseModel UpdateUser(SysUser UserInfo)
        {
            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 3);

                var cur = SessionService.GetSessionInfo(HttpContext.Session);
                if (UserInfo.PasswordHash != null
                    && cur.UserNumber != UserInfo.UserNumber
                    && cur.Permission < 3)
                { // only own can modify the password.
                    UserInfo.PasswordHash = null;
                }
                return new SuccessResponseModel()
                {
                    Message = "Success",
                    obj = ManageService.UpdateUser(UserInfo),
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
        public void Delete([FromBody] SysUser sysUser)
        {
            if (SessionService.GetSessionInfo(HttpContext.Session).Permission < 3) return;
            ManageService.DeleteUser(sysUser);
        }
        [HttpGet]
        public ResponseModel GetUsersByCourse([FromQuery] CourseOffering course,
                                              [FromQuery] SysUser user)
        {
            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 2);

                return new SuccessResponseModel()
                {
                    obj = ManageService.QueryUsers(user, course),
                    Message = "success",
                };
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message
                };
            }
        }
        [HttpPost]
        public ResponseModel Relation([FromBody] RelationModel relationModel)
        {
            try
            {
                AuthenticationService.Authorization(HttpContext.Session, 3);

                string msg = "";
                if (relationModel.User != null
                    && (relationModel.CourseAddList != null
                        || relationModel.CourseRemoveList != null))
                {
                    RelationService.UpdateRelation(
                                    relationModel.User,
                                    relationModel.CourseAddList,
                                    relationModel.CourseRemoveList);
                    Debug.WriteLine("user operation");
                }
                if (relationModel.CourseOffering != null
                     && (relationModel.UserAddList != null
                        || relationModel.UserRemoveList != null))
                {
                    RelationService.UpdateRelation(
                                    relationModel.CourseOffering,
                                    relationModel.UserAddList,
                                    relationModel.UserRemoveList);
                    Debug.WriteLine("course operation");
                }

                return new SuccessResponseModel()
                {
                    Message = msg,
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

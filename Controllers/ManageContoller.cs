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
        public ManageController()
        {
            ManageService = ManageService.GetInstance();
            RelationService = RelationService.GetInstance();
        }
        private readonly ManageService ManageService;
        private readonly RelationService RelationService;

        [HttpPost]
        public ResponseModel AddUser([FromBody] SysUser NewUser)
        {
            // authorization
            /*if (NewUser.Permission < 0
                || !AuthenticationService
                   .AuthorizationLevel(HttpContext.Session, NewUser))
            {
                return new FailureResponseModel()
                {
                    Message = "invalid permission",
                };
            }*/

            try
            {
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
            int Permission = 3;// SessionService.GetSessionInfo(HttpContext.Session).Permission;
            if (Permission < PrivateInfo.Permission || Permission < 0)
            {
                return new FailureResponseModel()
                {
                    Message = "NO PERMISSION",
                };
            }

            try
            {
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
            /*if (UserInfo.Permission < 0
                || !AuthenticationService.
                   AuthorizationLevel(HttpContext.Session, UserInfo))
            {
                return new FailureResponseModel()
                {
                    Message = "invalid permission",
                };
            }*/

            try
            {
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
            ManageService.DeleteUser(sysUser);
        }
        [HttpGet]
        public ResponseModel GetUsersByCourse([FromQuery] CourseOffering course,
                                              [FromQuery] SysUser user)
        {
            try
            {
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

using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using WebAPI.Model;
using SqlSugar.Extensions;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public void SetSessionInfo(PrivateInfoModel UserInfo)
        {
            HttpContext.Session.SetInt32("Permission", UserInfo.Permission);
            HttpContext.Session.SetString("Email", UserInfo.Email);
            if (UserInfo.UserName[0] != null)
                HttpContext.Session.SetString("Firstname", UserInfo.UserName[0]);
            if (UserInfo.UserName[2] != null)
                HttpContext.Session.SetString("Lastname", UserInfo.UserName[2]);
        }

        public PrivateInfoModel GetSessionInfo()
        {
            PrivateInfoModel info = new PrivateInfoModel();
            try
            {
                info.Permission = (int)HttpContext.Session.GetInt32("Permission");
                info.Email = HttpContext.Session.GetString("Email");
                info.UserName[0] = HttpContext.Session.GetString("Firstname");
                info.UserName[1] = HttpContext.Session.GetString("Lastname");
                return info;
            }
            catch
            {
                return info;
            }
        }
        [HttpPost]
        public ResponseModel Post([FromBody] SysUser Credential)
        {
            PrivateInfoModel User = GetSessionInfo();
            string msg = "Session Success";

            if (GetSessionInfo().Permission > 0) goto SuccessLogin;
            try
            {
                User = AuthenticationService.Login(Credential.Email, Credential.PasswordHash);
                SetSessionInfo(User);
                msg = "Password success";
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
SuccessLogin:
            return new SuccessResponseModel()
            {
                Message = msg,
                obj = new PublicInfoModel()
                {
                    UserName = User.UserName,
                    UserNumber = User.UserNumber,
                    Email = User.Email,
                    Permission = User.Permission,
                },
            };
        }
    }

}


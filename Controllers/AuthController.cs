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
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        public void SetSessionInfo(PublicInfoModel UserInfo)
        {
            try
            {
                HttpContext.Session.SetInt32("Permission", UserInfo.Permission);
                HttpContext.Session.SetString("Email", UserInfo.Email);
                HttpContext.Session.SetString("Firstname", UserInfo.UserName[0]);
                HttpContext.Session.SetString("Middlename", UserInfo.UserName[1]);
                HttpContext.Session.SetString("Lastname", UserInfo.UserName[2]);
            }
            catch
            { }
        }

        public PublicInfoModel GetSessionInfo()
        {
            PublicInfoModel info = new PublicInfoModel();
            try
            {
                info.Permission = (int)HttpContext.Session.GetInt32("Permission");
                info.Email = HttpContext.Session.GetString("Email");
                info.UserName = new string[3]
                {
                    HttpContext.Session.GetString("Firstname"),
                    HttpContext.Session.GetString("Middlename"),
                    HttpContext.Session.GetString("Lastname"),
                };
                return info;
            }
            catch
            {
                return info;
            }
        }
        [HttpPost]
        public ResponseModel Password([FromBody] SysUser Credential)
        {
            PublicInfoModel User = GetSessionInfo();
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
            PublicInfoModel PublicInfo = new PublicInfoModel()
            {
                UserName = User.UserName,
                UserNumber = User.UserNumber,
                Email = User.Email,
                Permission = User.Permission,
            };
            return new SuccessResponseModel()
            {
                Message = msg,
                obj = new object[2]
                {
                    PublicInfo,
                    SecurityService.CreateJWT(PublicInfo),
                }
            };
        }
        public ResponseModel Token([FromBody] string token)
        {
            try
            {
                PublicInfoModel PublicInfo = SecurityService.ValidateJWT(token);
                SetSessionInfo(PublicInfo);
                return new SuccessResponseModel()
                {
                    Message = "Success Token Login",
                    obj = new object[2]
                    {
                        PublicInfo,
                        SecurityService.CreateJWT(PublicInfo),
                    }
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
        public void Logout()
        {
            HttpContext.Session.Clear();
        }
    }

}


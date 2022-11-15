/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Chenrui Zhang
  Date   : 16/09/2022
******************************************/

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
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Password([FromBody] SysUser Credential)
        {

            PublicInfoModel User = SessionService.GetSessionInfo(HttpContext.Session);
            string msg = "Session Success";

            // fake login
            if (Credential.UserNumber == "123456"
             && Credential.PasswordHash == SecurityService.GetMD5Hash("123456"))
            {
                User.UserNumber = "Fake User";
                User.UserName = "123456";
                User.Email = "fake@fake.com";
                User.Permission = 3;
                goto SuccessLogin;
            }

            if (User.Permission > 0
                && User.UserNumber == Credential.UserNumber)
            {
                Debug.WriteLine(User.UserName);
                goto SuccessLogin;
            }
            try
            {
                User = AuthenticationService.GetInstance()
                    .Login(Credential.UserNumber, Credential.PasswordHash);
                SessionService.SetSessionInfo(HttpContext.Session, User);
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
                SysUserID = User.SysUserID,
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
        public ResponseModel Token(string token)
        {
            try
            {
                PublicInfoModel PublicInfo = SecurityService.ValidateJWT(token);
                SessionService.SetSessionInfo(HttpContext.Session,PublicInfo);
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

        [HttpDelete]
        public void Logout()
        {
            HttpContext.Session.Clear();
        }
    }

}


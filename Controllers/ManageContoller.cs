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
    [Route("[controller]")]
    public class ManageController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Post([FromBody] SysUser NewUser)
        {
            // authorization
            if (NewUser.Permission < 0)
            {
                return new FailureResponseModel()
                {
                    Message = "invalid permission",
                };
            }

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
                        Permission = 0,
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
        public void Get()
        {

        }

        [HttpPut]
        public void Put()
        {

        }

        [HttpDelete]
        public void Delete()
        {

        }
    }
}

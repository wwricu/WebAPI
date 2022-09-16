using WebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service;
using System.Dynamic;
using System.Diagnostics;
using SqlSugar;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManageController : ControllerBase
    {
        [HttpPost]
        public ExpandoObject Post([FromBody] SysUser NewUser)
        {
            dynamic response = new ExpandoObject();
            response.status = "failure";

            // authorization
            if (NewUser.Permission < 0)
            {
                response.msg = "invalid parameter";
            }

            try
            {
                ManageService.AddUser(NewUser);

                response.status = "success";
                response.permission = NewUser.Permission;
                response.MemberNumber = NewUser.MemberNumber;
                response.MemberName = new String[]
                {
                    NewUser.Firstname,
                    NewUser.Middlename,
                    NewUser.Lastname,
                };
                response.birthDate = NewUser.Birthdate;
                response.phone = NewUser.Phone;
                response.addresses = new String[]
                {
                    NewUser.AddressLine1,
                    NewUser.AddressLine2,
                    NewUser.AddressLine3,
                };
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                return response;
            }

            return response;
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

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAPI.Entity;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DocumentController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Upload([FromForm(Name="file")] List<IFormFile> files,
                                    [FromForm(Name="ApplicationID")] long applicationID,
                                    [FromForm(Name="Type")] string type)
        {
            try
            {
                Debug.WriteLine("File controller");
                Debug.WriteLine(type);
                Debug.WriteLine(applicationID.ToString());
                PublicInfoModel userInfo = SessionService.GetSessionInfo(HttpContext.Session);

                if (!ApplicationService
                        .UserHasPrivilege(userInfo.SysUserID,
                                          applicationID)) {
                    return new FailureResponseModel()
                    {
                        Message = "not your application!"
                    };
                }

                DocumentService
                    .GetInstance()
                    .AddDocuments(files,
                                  applicationID,
                                  type,
                                  userInfo.UserName);
                return new SuccessResponseModel();
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message
                };
            }
        }

        [HttpDelete]
        public ResponseModel Delete([FromBody] Document document)
        {
            try
            {
                DocumentService
                   .GetInstance().DeleteDocument(document.DocumentID);
                return new SuccessResponseModel();
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message
                };
            }
        }
    }
}

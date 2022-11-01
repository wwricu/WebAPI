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

                files.ForEach(file =>
                {
                    Debug.WriteLine(file.FileName);
                });
                DocumentService
                    .GetInstance()
                    .AddDocuments(files, 0, "testType", "testuser");
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

using K4os.Hash.xxHash;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using WebAPI.Entity;
using WebAPI.Model;
using WebAPI.Service;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DocumentController : ControllerBase
    {
        public DocumentController()
        {
            ApplicationService = ApplicationService.GetInstance();
        }
        private readonly ApplicationService ApplicationService;
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
                                  type);
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
    /*        [HttpGet]
            public HttpResponseMessage Get([FromQuery] Document document)
            {
                try
                {
                    var documentService = DocumentService.GetInstance();
                    var documents = documentService.GetDocuments(document);
                    if (documents == null || documents.Count == 0)
                    {
                        return new HttpResponseMessage();
                    }
                    Debug.WriteLine(documents[0].Url);
                    var filestream = new FileStream(documents[0].Url, FileMode.Open);
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(filestream)
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = documents[0].Title
                    };
                    return response;
                }
                catch
                {
                    return new HttpResponseMessage();
                }
            }*/
        [HttpGet]
        public IActionResult? Get([FromQuery] Document document)
        {
            try
            {
                var documentService = DocumentService.GetInstance();
                var documents = documentService.GetDocuments(document);
                if (documents == null || documents.Count == 0)
                {
                    return NotFound();
                }
                // Debug.WriteLine(documents[0].Url);
                var filestream = new FileStream(documents[0].Url, FileMode.Open);
                return File(filestream,
                            "application/octet-stream",
                            documents[0].Title);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}

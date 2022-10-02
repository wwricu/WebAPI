using Microsoft.AspNetCore.Mvc;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Upload([FromForm(Name = "file")] List<IFormFile> files)
        {
            try
            {
                files.ForEach(file =>
                {
                    var fileName = file.FileName;
                    //get suffix
                    string fileExtension = file.FileName[(file.FileName.LastIndexOf(".") + 1)..];
                    // save file
                    var stream = file.OpenReadStream();
                    //convert stream to byte[]
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    // set stream beginning
                    stream.Seek(0, SeekOrigin.Begin);
                    // write byte[] to file
                    var fs = new FileStream("C:\\Users\\wang.weiran\\Documents\\code\\INFT6900\\FileFolder"
                                            + file.FileName,
                                            FileMode.Create);
                    var bw = new BinaryWriter(fs);
                    bw.Write(bytes);
                    bw.Close();
                    fs.Close();
                });
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

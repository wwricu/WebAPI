using System.Diagnostics;
using System.Net.Mail;
using System.Security.Policy;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class DocumentService
    {
        private DocumentService()
        {
            UploadRootPath = SysConfigModel
                            .Configuration
                            .GetConnectionString("UploadRootPath");
            DocumentDAO = new DocumentDAO();
        }
        private static DocumentService Instance = new DocumentService();
        public static DocumentService GetInstance()
        {
            Instance ??= new DocumentService();
            return Instance;
        }
        private readonly string? UploadRootPath;
        private DocumentDAO DocumentDAO { get; set; }

        public void AddDocuments(List<IFormFile> files,
                                 long applicationID,
                                 string type,
                                 string folderName)
        {
            // TODO: authorize the application
            var uploadPath = UploadRootPath + "\\"
                           + folderName + "\\"
                           + applicationID.ToString() + "\\";
            if (!Directory.Exists(uploadPath))            　　              
                Directory.CreateDirectory(uploadPath);

            var documents = new List<Document>();
            files.ForEach(file =>
            {
                var doc = new Document()
                {
                    ApplicationID = applicationID,
                    Type = type,
                    Title = file.FileName,
                };
                var fileName = file.FileName;
                // save file
                var stream = file.OpenReadStream();
                //convert stream to byte[]
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                // set stream beginning
                stream.Seek(0, SeekOrigin.Begin);
                // write byte[] to file
                var fs = new FileStream(uploadPath
                                        + file.FileName,
                                        FileMode.Create);
                var bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
            });

            DocumentDAO.Insert(documents);
        }

        public void deleteDocument(int documentID)
        {

        }
    }
}

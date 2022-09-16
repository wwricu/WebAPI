using WebAPI.DAO;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class ManageService
    {
        public static int AddUser(CredentialInfoModel NewUser)
        {
            if (NewUser.Permission < 0 || !UtilService.IsValidEmail(NewUser.Email))
            {
                throw new Exception("invalid email");
            }

            string Salt = SecurityService.GenerateSalt();
            string PasswordHash = SecurityService.GetMD5Hash(NewUser.Salt + NewUser.PasswordHash);

            UserDAO userDAO = new();
            if (NewUser.Permission == 0)
            {
                return userDAO.Insert(new Student(NewUser)
                {
                    Salt = Salt,
                    PasswordHash = PasswordHash,
                });
            }
            else
            {
                return userDAO.Insert(new Staff(NewUser)
                {
                    Salt = Salt,
                    PasswordHash = PasswordHash,
                });
            }
        }
    }
}

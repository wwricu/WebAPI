using WebAPI.DAO;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class ManageService
    {
        public static int AddUser(SysUser NewUser)
        {
            if (NewUser.Permission < 0 || !UtilService.IsValidEmail(NewUser.Email))
            {
                throw new Exception("invalid email");
            }

            NewUser.Salt = SecurityService.GenerateSalt();
            NewUser.PasswordHash = SecurityService.GetMD5Hash(NewUser.Salt + NewUser.PasswordHash);

            UserDAO userDAO = new();
            return userDAO.Insert(NewUser);
        }

        public static List<SysUser> QueryUsers(PrivateInfoModel PrivateInfo)
        {
            return new UserDAO().QueryUsers(PrivateInfo);
        }
    }
}

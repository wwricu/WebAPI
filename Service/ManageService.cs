using WebAPI.DAO;
using WebAPI.Entity;

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

            UserDAO userDAO = new UserDAO();
            if (NewUser.Permission == 0)
            {
                return userDAO.Insert(new Student(NewUser));
            }
            else
            {
                return userDAO.Insert(new Staff(NewUser));
            }
        }
    }
}

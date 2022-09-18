using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AuthenticationService
    {
        public static SysUser Login(string UserNumber, string Password)
        {
            UserDAO UserDAO = new UserDAO();
            var UserList = UserDAO.QueryUserByNumber(UserNumber);
            if (UserList.Count() == 0)
            {
                throw new Exception("NO member found");
            }
            SysUser User = UserList[0];

            if (User.PasswordHash 
                == SecurityService.GetMD5Hash(User.Salt + Password))
            {
                return User;
            }
            else
            {
                throw new Exception("Wrong Password");
            }
        }

        public static bool AuthorizationLevel(ISession session, PublicInfoModel UserInfo)
        {
            try
            {
                Debug.WriteLine(UserInfo.UserNumber);
                int Permission = (int)session.GetInt32("Permission");
                return Permission > UserInfo.Permission;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

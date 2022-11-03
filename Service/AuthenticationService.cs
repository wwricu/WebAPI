using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AuthenticationService
    {
        private AuthenticationService()
        {
        }
        private static AuthenticationService Instance = new();
        public static AuthenticationService GetInstance()
        {
            Instance ??= new AuthenticationService();
            return Instance;
        }

        public SysUser Login(string UserNumber, string Password)
        {
            var UserList = new UserDAO().QueryUserByNumber(UserNumber);
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
                return (int)session.GetInt32("Permission") > UserInfo.Permission;
            }
            catch
            {
                return false;
            }
        }
        /*
         @level:
         0 to allow all user,
         1 to restrict student and allow staff,
         2 to restrict staff and ban student,
         user cannot access level HIGHER than their permission
         @Obj: objects to handle
        */
        public static void Authorization(ISession session, int level)
        {
            var currentUser = SessionService.GetSessionInfo(session);


            if (currentUser.Permission == 3 || currentUser.Permission >= level)
            {
                return;
            }

            Debug.WriteLine("Authorization failed");
            Debug.WriteLine(currentUser.Permission);
            throw new AuthenticationException("No permission");
        }
    }
}

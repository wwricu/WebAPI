using System.Diagnostics;
using System.Security.Authentication;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class SessionService
    {
        public static void SetSessionInfo(ISession session, PublicInfoModel UserInfo)
        {
            try
            {
                session.SetInt32("SysUserID", UserInfo.SysUserID);
                session.SetInt32("Permission", UserInfo.Permission);
                if (UserInfo.Email != null)
                    session.SetString("Email", UserInfo.Email);
                if (UserInfo.UserNumber != null)
                    session.SetString("UserNumber", UserInfo.UserNumber);
                if (UserInfo.UserName != null)
                    session.SetString("UserName", UserInfo.UserName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static PublicInfoModel GetSessionInfo(ISession session)
        {
            var info = new PublicInfoModel();
            try
            {
                var SysUserID = session.GetInt32("SysUserID");
                info.SysUserID = SysUserID == null ? 0 : (int)SysUserID;
                var Permission = session.GetInt32("Permission");
                info.Permission = Permission == null ? 0 : (int)Permission;
                info.Email = session.GetString("Email");
                info.UserNumber = session.GetString("UserNumber");
                info.UserName = session.GetString("UserName");
                return info;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return info;
            }
        }

        public static void isOwner(ISession session, Application application)
        {
            var currentUser = GetSessionInfo(session);

            if (application.ApplicationID != 0)
            {
                application = new ApplicationDAO().Query(new Application()
                {
                    ApplicationID = application.ApplicationID,
                }, null, null)[0];
            }

            if (currentUser.Permission == 3
                || currentUser.Permission == 2 && application.StaffID == currentUser.SysUserID
                || currentUser.Permission == 1 && application.StudentID == currentUser.SysUserID)
            {
                return;
            }

            Debug.WriteLine("not a owner"); // TODO
            Debug.WriteLine(currentUser.SysUserID);
            Debug.WriteLine(application.StaffID);
            Debug.WriteLine(application.StudentID);

            throw new AuthenticationException("Not your Application");
        }
    }
}

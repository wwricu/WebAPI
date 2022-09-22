using System.Diagnostics;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class SessionService
    {
        public static void SetSessionInfo(ISession session, PublicInfoModel UserInfo)
        {
            try
            {
                session.SetInt32("Permission", UserInfo.Permission);
                if (UserInfo.Email != null)
                    session.SetString("Email", UserInfo.Email);
                if (UserInfo.UserNumber != null)
                    session.SetString("UserNumber", UserInfo.UserNumber);
                if (UserInfo.UserName != null)
                    session.SetString("UserNumber", UserInfo.UserName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static PublicInfoModel GetSessionInfo(ISession session)
        {
            PublicInfoModel info = new PublicInfoModel();
            try
            {
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
    }
}

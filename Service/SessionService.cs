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
                session.SetString("Email", UserInfo.Email);
                session.SetString("UserNumber", UserInfo.UserNumber);
                session.SetString("Firstname", UserInfo.UserName[0]);
                session.SetString("Middlename", UserInfo.UserName[1]);
                session.SetString("Lastname", UserInfo.UserName[2]);
            }
            catch
            { }
        }

        public static PublicInfoModel GetSessionInfo(ISession session)
        {
            PublicInfoModel info = new PublicInfoModel();
            try
            {
                info.Permission = (int)session.GetInt32("Permission");
                info.Email = session.GetString("Email");
                info.UserNumber = session.GetString("UserNumber");
                info.UserName = new string[3]
                {
                    session.GetString("Firstname"),
                    session.GetString("Middlename"),
                    session.GetString("Lastname"),
                };
                return info;
            }
            catch
            {
                return info;
            }
        }
    }
}

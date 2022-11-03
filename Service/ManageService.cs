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

            return new UserDAO().Insert(NewUser);
        }

        public static List<SysUser> QueryUsers(SysUser user, CourseOffering course)
        {
            return new UserDAO().QueryUsers(user, course, true);
        }
        public static List<SysUser> QueryUsers(PrivateInfoModel PrivateInfo)
        {
            return new UserDAO().QueryUsers(PrivateInfo, null, true);
        }
        public static List<SysUser> QueryCandidateUsers(PrivateInfoModel user,
                                                        CourseOffering course)
        {
            return new UserDAO().QueryUsers(user, course, false);
        }
        public static bool UpdateUser(SysUser UserInfo)
        {
            return new UserDAO().UpdateUser(UserInfo);
        }
        public static void DeleteUser(SysUser sysUser)
        {

            new UserDAO().DeleteUser(sysUser);

            // remove related assessment instances
            if (sysUser.Permission != 1) return;
            
            var assessmentDAO = new AssessmentDAO();
            var applicationDAO = new ApplicationDAO();
                
            var assessmentInstances = assessmentDAO.Query(sysUser, null);
            assessmentDAO.Delete(null, assessmentInstances);

            var applications = applicationDAO.Query(null, sysUser, null);
            applicationDAO.Delete(applications);
        }
        public static SysUser findStudentService()
        {
            return new UserDAO()
                .QueryUsers(new PrivateInfoModel()
                {
                    Permission = 3
                }, null, true)[0];
        }
    }
}

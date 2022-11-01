using WebAPI.DAO;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class ManageService
    {
        private ManageService()
        {
            UserDAO = new();
            AssessmentDAO = new();
            ApplicationDAO = new();
        }
        private static ManageService Instance = new();
        public static ManageService GetInstance()
        {
            Instance ??= new ManageService();
            return Instance;
        }

        private readonly UserDAO UserDAO;
        private readonly AssessmentDAO AssessmentDAO;
        private readonly ApplicationDAO ApplicationDAO;


        public int AddUser(SysUser NewUser)
        {
            if (NewUser.Permission < 0 || !UtilService.IsValidEmail(NewUser.Email))
            {
                throw new Exception("invalid email");
            }

            NewUser.Salt = SecurityService.GenerateSalt();
            NewUser.PasswordHash = SecurityService.GetMD5Hash(NewUser.Salt + NewUser.PasswordHash);

            return UserDAO.Insert(NewUser);
        }

        public List<SysUser> QueryUsers(SysUser user, CourseOffering course)
        {
            return UserDAO.QueryUsers(user, course, true);
        }
        public List<SysUser> QueryUsers(PrivateInfoModel PrivateInfo)
        {
            return UserDAO.QueryUsers(PrivateInfo, null, true);
        }
        public List<SysUser> QueryCandidateUsers(PrivateInfoModel user,
                                                        CourseOffering course)
        {
            return UserDAO.QueryUsers(user, course, false);
        }
        public bool UpdateUser(SysUser UserInfo)
        {
            return UserDAO.UpdateUser(UserInfo);
        }
        public void DeleteUser(SysUser sysUser)
        {

            UserDAO.DeleteUser(sysUser);

            // remove related assessment instances
            if (sysUser.Permission == 1)
            {
                var assessmentInstances = AssessmentDAO.Query(sysUser, null);
                AssessmentDAO.Delete(null, assessmentInstances);

                var applications = ApplicationDAO.Query(null, sysUser, null);
                ApplicationDAO.Delete(applications);
            }
        }
    }
}

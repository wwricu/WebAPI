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
        }
        private static ManageService Instance = new();
        public static ManageService GetInstance()
        {
            Instance ??= new ManageService();
            return Instance;
        }

        private readonly UserDAO UserDAO;
        
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
        public void DeleteUser(string userNumber)
        {
            UserDAO.DeleteUser(userNumber);
            var user = new SysUser()
            {
                UserNumber = userNumber,
            };
            // remove related assessment instances
            var assessmentDAO = new AssessmentDAO();
            var assessmentInstances = assessmentDAO.Query(user, null);
            assessmentDAO.Delete(null, assessmentInstances);
        }
    }
}

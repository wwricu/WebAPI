using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AuthenticationService
    {
        public static SysUser Login(string Email, string Password)
        {
            UserDAO UserDAO = new UserDAO();
            SysUser User;
            var StaffList = UserDAO.QueryStaffByEmail(Email);
            if (StaffList.Count() == 0)
            {
                var StudentList = UserDAO.QueryStudentByEmail(Email);
                if (StudentList.Count() == 0)
                {
                    throw new Exception("NO member found");
                }
                else
                {
                    User = StudentList[0];
                }
            }
            else
            {
                User = StaffList[0];
            }

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
    }
}

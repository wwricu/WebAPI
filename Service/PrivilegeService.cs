using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class PrivilegeService
    {
        public static int InsertPrivilege(Privilege Privilege)
        {
            return new PrivilegeDAO().Insert(Privilege);
        }
        public static void UpdatePrivilege(Privilege Privilege)
        {
            PrivilegeDAO PrivilegeDAO = new();
            PrivilegeDAO.Update(Privilege);
        }
        public static List<Privilege> QueryPrivilege(Privilege Privilege,
                                                     SysUser Staff,
                                                     CourseOffering Course)
        {
            int StaffID = new UserDAO().QueryUsers(Staff, 0, 0)[0].SysUserID;
            var CourseOfferingID = new CourseOfferingDAO().Query(Course)[0].CourseOfferingID;
            return new PrivilegeDAO().Query(Privilege, StaffID, CourseOfferingID);
        }

        public static void AttachCourseOffering(Privilege Privilege,
                                                CourseOffering courseOffering)
        {

        }
    }
}

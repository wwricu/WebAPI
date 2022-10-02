using SqlSugar;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class ApplicationService
    {
        public static void Save(Application application)
        {
            application.Status = "Draft";
            new ApplicationDAO().Insert(application);
        }
        public static void Submit(Application application)
        {
            application.Status = "Pending";
            // assign a staff
            // send mail to staff
            new ApplicationDAO().Insert(application);
        }
        public static void Pending(Application application)
        {
            application.Status = "Pending";
            // send mail to student
        }
        public static void Reject(Application application)
        {
            application.Status = "Reject";
            // send mail to student
        }
        public static void Approve(Application application)
        {
            application.Status = "Approve";
            // do change to assessment instance
            // send mail to student
        }
        public static void Assign(Application application, SysUser Staff)
        {
            application.Status = "Approve";
            application.StaffID = Staff.SysUserID;
            // do change to assessment instance
            // send mail to staff
        }
        public static void Delete(Application application)
        {
            // cascade delete
            new ApplicationDAO().Delete(application);
        }
    }
}

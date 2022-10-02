using SqlSugar;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class ApplicationService
    {
        /* Student service start */
        public static void Save(Application application)
        {
            application.Status = "Draft";
            new ApplicationDAO().Insert(application);
        }
        public static void Delete(Application application)
        {
            if (application.Status != "Draft") return;
            new ApplicationDAO().Delete(application);
        }
        // TODO: return reference number
        public static long Submit(Application application)
        {
            var course = new CourseOfferingDAO()
                            .Query(null,
                                   null,
                                   new AssessmentInstance()
                                   {
                                       AssessmentID = application.InstanceID,
                                   },
                                   true)
                            .First();

            if (course == null)
            {
                throw new Exception("No course find");
            }

            var staff = new UserDAO()
                           .QueryUsers(new SysUser()
                           {
                               Permission = 2,
                           },
                           course,
                           true)
                           .First();
            var appDAO = new ApplicationDAO();
            long refNum;
            
            application.Status = "Pending";
            application.StaffID = staff.SysUserID;

            // TODO: permission issue to update other's application
            // should be solved in controller
            if (application.ApplicationID != 0)
            {
                appDAO.Update(application);
                refNum = application.ApplicationID;
            }
            else
            {
                refNum = appDAO.Insert(application);
            }

            MailService.GetInstance().SendMail(staff.Email,
                                               null,
                                               "New application submitted",
                                               "");

            return refNum;
        }
        /* Student service end */
        /* Staff service start */
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
        /* Staff service end */
    }
}

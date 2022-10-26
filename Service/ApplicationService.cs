using SqlSugar;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class ApplicationService
    {
        public static List<Application> Query(Application application)
        {
            return new ApplicationDAO().Query(application,
                                              null,
                                              null);
        }
        /* Student service start */
        public static long Save(Application application)
        {
            application.Status = "Draft";
            application.SubmitDate = DateTime.Now.ToString();
            if (application.ApplicationID == 0)
            {
                return new ApplicationDAO().Insert(application);
            }
            else
            {
                return new ApplicationDAO().Update(application);
            }
        }
        public static void Delete(Application application)
        {
            if (application.Status != "Draft") return;
            new ApplicationDAO().Delete(application);
        }
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
        public static void ChangeState(Application application)
        {
            new ApplicationDAO().Update(application);
            MailService.GetInstance().SendMail(application.Student.Email,
                                   null,
                                   "Your application "
                                   + application.ApplicationID
                                   + " is changed to " + application.Status,
                                   "");
        }
        public static void Approve(Application application)
        {
            new ApplicationDAO().Update(application);
            new AssessmentDAO().Update(application.AssessmentInstance);
            MailService.GetInstance().SendMail(application.Student.Email,
                                   null,
                                   "Your application "
                                   + application.ApplicationID
                                   + " is Approved",
                                   "");
        }
        public static void Assign(Application application)
        {
            new ApplicationDAO().Update(application);
            MailService.GetInstance().SendMail(application.Staff.Email,
                                   null,
                                   "Application "
                                   + application.ApplicationID
                                   + " is assigned to you.",
                                   "");
        }
        /* Staff service end */
    }
}

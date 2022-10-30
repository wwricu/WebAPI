using SqlSugar;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class ApplicationService
    {
        public static List<Application> Query(Application application, SysUser user)
        {
            return new ApplicationDAO().Query(application,
                                              user,
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
            // if (application.Status != "Draft") return;
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

            application = new ApplicationDAO().Query(application, null, null)[0];
            application.Status = "Pending";
            application.StaffID = staff.SysUserID;

            // TODO: permission issue to update other's application
            // should be solved in controller
            application.SubmitDate = DateTime.Now.ToString();
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
            var applicationDAO = new ApplicationDAO();
            var oldApplication = applicationDAO.Query(new Application()
                    {
                        ApplicationID = application.ApplicationID,
                    },
                    null,null)[0];
            if (application.Status != null)
            {
                oldApplication.Status = application.Status;
            }
            if (application.StaffComment != null)
            {
                oldApplication.StaffComment = application.StaffComment;
            }
            if (application.StaffID != 0)
            {
                oldApplication.StaffID = application.StaffID;
            }
            applicationDAO.Update(oldApplication);
            var student = new UserDAO().QueryUserByNumber(oldApplication.StudentNumber)[0];
            MailService.GetInstance().SendMail(student.Email,
                                   null,
                                   "Your application "
                                   + oldApplication.ApplicationID
                                   + " is changed to " + oldApplication.Status,
                                   "");
        }
        public static void Approve(Application application)
        {
            new AssessmentDAO().Update(application.AssessmentInstance);
            application.AssessmentInstance = null;
            new ApplicationDAO().Update(application);
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

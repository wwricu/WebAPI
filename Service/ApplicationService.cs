using SqlSugar;
using System.Diagnostics;
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
            return application.ApplicationID == 0 ?
                new ApplicationDAO().Insert(application) :
                new ApplicationDAO().Update(application);
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

            var studentService = ManageService.findStudentService();
            var appDAO = new ApplicationDAO();
            long refNum;

            application = new ApplicationDAO().Query(application, null, null)[0];
            application.Status = "Pending";
            application.StaffID = studentService.SysUserID;

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

            _ = MailService.GetInstance().SendMail(studentService.Email,
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
            _ = MailService.GetInstance().SendMail(student.Email,
                                   null,
                                   "Your application "
                                   + oldApplication.ApplicationID
                                   + " is changed to " + oldApplication.Status,
                                   application.StaffComment);
        }
        public static void Approve(Application application)
        {
            application.Status = "Approved";
            new AssessmentDAO().Update(application.AssessmentInstance);
            application.AssessmentInstance = null; // to avoid nav update

            new ApplicationDAO().Update(application);
            _ = MailService.GetInstance().SendMail(application.Student!.Email,
                                   null,
                                   "Your application "
                                   + application.ApplicationID
                                   + " is Approved",
                                   "");
        }
        public static void Assign(Application application)
        {
            application.Status = "Assigned to CC";
            var staff = new UserDAO().QueryUsers(new PrivateInfoModel()
            {
                SysUserID = application.StaffID
            },null, true)[0];
            new ApplicationDAO().Update(application);
            Debug.WriteLine(staff.Email);
            Debug.WriteLine(staff.SysUserID);
            _ = MailService.GetInstance().SendMail(staff!.Email,
                                   null,
                                   "Application "
                                   + application.ApplicationID
                                   + " is assigned to you.",
                                   "");
        }
        /* Staff service end */

        public static bool UserHasPrivilege(int sysUserID, long applicationID)
        {
            var applications = new ApplicationDAO().Query(new Application()
            {
                ApplicationID = applicationID,
            }, null, null);
            Debug.WriteLine(applications[0].StudentID);
            Debug.WriteLine(sysUserID);

            if (applicationID != 0
                && applications != null
                && applications.Count == 1)
            {
                return applications[0].StudentID == sysUserID;
            }

            return false;
        }
    }
}

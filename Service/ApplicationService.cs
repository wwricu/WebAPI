using SqlSugar;
using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class ApplicationService
    {
        private ApplicationService()
        {
            UserDAO = new();
            ApplicationDAO = new();
            CourseOfferingDAO = new();
            AssessmentDAO = new();
        }
        private static ApplicationService Instance = new();
        public static ApplicationService GetInstance()
        {
            Instance ??= new ApplicationService();
            return Instance;
        }

        private readonly UserDAO UserDAO;
        private readonly ApplicationDAO ApplicationDAO;
        private readonly CourseOfferingDAO CourseOfferingDAO;
        private readonly AssessmentDAO AssessmentDAO;


        public List<Application> Query(Application application, SysUser user)
        {
            return ApplicationDAO.Query(application,
                                              user,
                                              null);
        }
        /* Student service start */
        public long Save(Application application)
        {
            application.Status = "Draft";
            application.SubmitDate = DateTime.Now.ToString();
            if (application.ApplicationID == 0)
            {
                return ApplicationDAO.Insert(application);
            }
            else
            {
                return ApplicationDAO.Update(application);
            }
        }
        public void Delete(Application application)
        {
            // if (application.Status != "Draft") return;
            ApplicationDAO.Delete(application);
        }
        public long Submit(Application application)
        {
            var course = CourseOfferingDAO
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

            var staff = UserDAO
                           .QueryUsers(new SysUser()
                           {
                               Permission = 2,
                           },
                           course,
                           true)
                           .First();
            var appDAO = ApplicationDAO;
            long refNum;

            application = ApplicationDAO.Query(application, null, null)[0];
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

            _ = MailService.GetInstance().SendMail(staff.Email,
                                               null,
                                               "New application submitted",
                                               "");

            return refNum;
        }
        /* Student service end */
        /* Staff service start */
        public void ChangeState(Application application)
        {
            var oldApplication = ApplicationDAO.Query(new Application()
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
            ApplicationDAO.Update(oldApplication);
            var student = UserDAO.QueryUserByNumber(oldApplication.StudentNumber)[0];
            _ = MailService.GetInstance().SendMail(student.Email,
                                   null,
                                   "Your application "
                                   + oldApplication.ApplicationID
                                   + " is changed to " + oldApplication.Status,
                                   application.StaffComment);
        }
        public void Approve(Application application)
        {
            AssessmentDAO.Update(application.AssessmentInstance);
            application.AssessmentInstance = null;
            ApplicationDAO.Update(application);
            _ = MailService.GetInstance().SendMail(application.Student.Email,
                                   null,
                                   "Your application "
                                   + application.ApplicationID
                                   + " is Approved",
                                   "");
        }
        public void Assign(Application application)
        {
            ApplicationDAO.Update(application);
            _ = MailService.GetInstance().SendMail(application.Staff.Email,
                                   null,
                                   "Application "
                                   + application.ApplicationID
                                   + " is assigned to you.",
                                   "");
        }
        /* Staff service end */

        public bool UserHasPrivilege(int sysUserID, long applicationID)
        {
            var applications = ApplicationDAO.Query(new Application()
            {
                ApplicationID = applicationID,
            }, null, null);
            Debug.WriteLine(applications[0].StudentID);
            Debug.WriteLine(sysUserID);

            if (applications != null && applications.Count == 1)
            {
                return applications[0].StudentID == sysUserID;
            }

            return false;
        }
    }
}

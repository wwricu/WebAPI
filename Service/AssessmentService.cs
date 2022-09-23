using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AssessmentService
    {
        public static void Insert(CourseOffering course,
                                  Assessment template)
        {
            template.CourseOfferingID = course.CourseOfferingID;
            template.Status = "TEMPLATE";
            var assessmentDAO = new AssessmentDAO();
            assessmentDAO.Insert(new List<Assessment>()
                                {
                                    template,
                                });

            var studentList = new UserDAO()
                                 .QueryUsers(new SysUser()
                                 {
                                     Permission = 1,
                                 },
                                 course,
                                 true);

            var instanceList = new List<Assessment>();
            foreach (SysUser student in studentList)
            {
                instanceList.Add(new Assessment()
                {
                    Name = template.Name,
                    Type = template.Type,
                    BeginDate = template.BeginDate,
                    EndDate = template.EndDate,
                    Status = "TO DO",
                    BaseAssessmentID = template.AssessmentID,
                });
            }
            assessmentDAO.Insert(instanceList);
        }
        public static List<Assessment> QueryTemplates(CourseOffering course)
        {
            return new List<Assessment>();
        }
        public static List<Assessment> QueryInstance(SysUser student)
        {
            var list = new List<Assessment>();
            return list;
        }
        public static void DeleteTemplate(Assessment assessment)
        {
            new AssessmentDAO().Delete(assessment);
        }
    }
}

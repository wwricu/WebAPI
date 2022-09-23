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
                instanceList.Add(new Assessment(template, student));
            }
            assessmentDAO.Insert(instanceList);
        }
        public static void Insert(SysUser student, Assessment template)
        {
            new AssessmentDAO().Insert(new List<Assessment>()
            {
                new Assessment(template, student),
            });
        }
        public static void Update(Assessment assessment)
        {
            var instanceList = new List<Assessment>();
            if (assessment.BaseAssessmentID != 0)
            {
                instanceList.Add(new Assessment()
                {
                    AssessmentID = assessment.BaseAssessmentID,
                });
            }
            var AssessmentDAO = new AssessmentDAO();
            AssessmentDAO.Update(instanceList);
            AssessmentDAO.Update(new List<Assessment> { assessment });
        }
        public static List<Assessment> QueryTemplates(CourseOffering course)
        {
            return new AssessmentDAO().Query(new Assessment(),
                                             new SysUser(),
                                             course);
        }
        public static List<Assessment> QueryInstance(SysUser student)
        {
            return new AssessmentDAO().Query(new Assessment(),
                                             student,
                                             new CourseOffering());
        }
        public static void Delete(List<Assessment> assessments)
        {
            var instanceList = new List<Assessment>();
            foreach (var assessment in assessments)
            {
                if (assessment.BaseAssessmentID != 0)
                {
                    instanceList.Add(new Assessment() {
                        AssessmentID = assessment.BaseAssessmentID,
                    });
                }
            }
            assessments.AddRange(instanceList);
            new AssessmentDAO().Delete(assessments);
        }
    }
}

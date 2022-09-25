using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AssessmentService
    {
        public static void Insert(AssessmentTemplate template)
        {
            var assessmentDAO = new AssessmentDAO();
            var course = new CourseOffering() { CourseOfferingID = template.CourseOfferingID};

            var studentList = new UserDAO()
                                 .QueryUsers(new SysUser()
                                 {
                                     Permission = 1,
                                 },
                                 course,
                                 true);
            course = new CourseOfferingDAO().Query(course).First();

            template.AssessmentID = Guid.NewGuid().ToString();
            template.CourseOfferingName = course.CourseName + " "
                + course.Semester + " " + course.Year;

            var instanceList = new List<AssessmentInstance>();
            foreach (SysUser student in studentList)
            {
                instanceList.Add(new AssessmentInstance(template, student));
            }
            assessmentDAO.Insert(new List<AssessmentTemplate>() {template},
                                                    instanceList);
        }
        public static void Update(Assessment assessment)
        {
            var assessmentDAO = new AssessmentDAO();

            if (assessment is AssessmentTemplate) {
                var instanceList = assessmentDAO.Query(null, assessment);
                foreach (var instance in instanceList)
                {
                    instance.Name = assessment.Name;
                    instance.Type = assessment.Type;
                    instance.BeginDate = assessment.BeginDate;
                    instance.EndDate = assessment.EndDate;
                    instance.Location = assessment.Location;
                }
                assessmentDAO.Update(null, instanceList);
            }

            assessmentDAO.Update(assessment);
        }
        public static List<AssessmentTemplate> QueryTemplates(CourseOffering course)
        {
            return new AssessmentDAO().Query(null, course);
        }
        public static List<AssessmentInstance> QueryInstance(SysUser student)
        {
            student.SysUserID = new UserDAO().QueryUsers(student, null, true).First().SysUserID;
            return new AssessmentDAO().Query(student, null);
        }
        public static void Delete(List<AssessmentTemplate> templates)
        {
            var assessmentDAO = new AssessmentDAO();
            var instanceList = new List<AssessmentInstance>();
            
            foreach (var template in templates)
            {
                instanceList.AddRange(assessmentDAO.Query(null, template));
            }

            Debug.WriteLine("delete");
            foreach(var instance in instanceList)
            {
                Debug.WriteLine(instance.Name);
            }
            
            assessmentDAO.Delete(templates, instanceList);
        }
        public static void Attach(CourseOffering course,
                                  SysUser student)
        {
            var instances = new List<AssessmentInstance>();
            var templates = QueryTemplates(course);
            foreach (var template in templates)
            {
                instances.Add(new AssessmentInstance(template, student));
            }
            new AssessmentDAO().Insert(null, instances);
        }
        public static void Detach(CourseOffering course,
                                  SysUser students)
        {
            var instances = QueryInstance(students);
            var forDelete = new List<AssessmentInstance>();

            foreach (var instance in instances)
            {
                if (instance.CourseOfferingID == course.CourseOfferingID)
                {
                    forDelete.Add(instance);
                }
            }

            new AssessmentDAO().Delete(null, instances);
        }
    }
}

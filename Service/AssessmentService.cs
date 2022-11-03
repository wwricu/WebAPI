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
                    instance.BeginTime = assessment.BeginTime;
                    instance.BeginDate = assessment.BeginDate;
                    instance.EndTime = assessment.EndTime;
                    instance.EndDate = assessment.EndDate;
                    instance.Location = assessment.Location;
                }
                assessmentDAO.Update(null, instanceList);
            }

            assessmentDAO.Update(assessment);
        }
        public static List<AssessmentInstance> Query(AssessmentInstance instance)
        {
            return new AssessmentDAO().Query(instance);
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
            var forInsDelete = new List<AssessmentInstance>();
            var forAppDelete = new List<Application>();

            foreach (var instance in instances)
            {
                if (instance.CourseOfferingID == course.CourseOfferingID)
                {
                    forInsDelete.Add(instance);
                }
            }

            var applicationDAO = new ApplicationDAO();
            foreach (var instance in forInsDelete)
            {
                forAppDelete.AddRange(applicationDAO.Query(null, null, instance));
            }
            applicationDAO.Delete(forAppDelete);
            // Do not delete assessment instances
            new AssessmentDAO().Delete(null, instances);
        }
    }
}

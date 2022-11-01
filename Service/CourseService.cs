using Microsoft.AspNetCore.Mvc;
using WebAPI.DAO;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class CourseService
    {
        private CourseService()
        {
            CourseOfferingDAO = new();
            AssessmentService = AssessmentService.GetInstance();
        }
        private static CourseService Instance = new();
        public static CourseService GetInstance()
        {
            Instance ??= new CourseService();
            return Instance;
        }

        private readonly CourseOfferingDAO CourseOfferingDAO;
        private readonly AssessmentService AssessmentService;


        public void Insert(CourseOffering course)
        {
            CourseOfferingDAO.Insert(course);
        }
        public void Update(CourseOffering course)
        {
            CourseOfferingDAO.Update(course);
        }
        public void Delete(CourseOffering course)
        {
            // all related assessment templates
            AssessmentService.Delete(AssessmentService.QueryTemplates(course));
            CourseOfferingDAO.Delete(course);
        }
        public List<CourseOffering> Query(CourseOffering Course,
                                                 SysUser user,
                                                 Assessment assessment)
        {
            /*return CourseOfferingDAO.TestQuery();*/
            return CourseOfferingDAO.Query(Course, user, assessment, true);
        }
        public List<CourseOffering> QueryCandidates(CourseOffering Course,
                                         SysUser user,
                                         Assessment assessment)
        {
            return CourseOfferingDAO.Query(Course, user, assessment, false);
        }
        public List<CourseOffering> QueryMultiple(CourseQueryModel model)
        {
            return CourseOfferingDAO
                      .QueryMultiple(model.Years,
                                     model.Semesters,
                                     model.Names);
        }
    }
}

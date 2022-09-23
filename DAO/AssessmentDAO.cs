using SqlSugar;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class AssessmentDAO
    {
        private SqlSugarClient db;
        public AssessmentDAO()
        {
            db = UtilService.GetDBClient();
        }
        public void Insert(List<Assessment> assessments)
        {
            db.Insertable(assessments);
        }
        public void Update(Assessment assessment)
        {
            db.Updateable(assessment);
        }
        // delete template/instance assessment
        public void Delete(List<Assessment> assessments)
        {
            if (assessments != null)
                db.Deleteable(assessments);
        }
        // use student to query instances, no student to query templates
        // this is to say, templates have StudentID to 0, instances have non-0
        public List<Assessment> Query(Assessment assessment,
                                      SysUser student,
                                      CourseOffering course)
        {
            var res = db.Queryable<Assessment>();

            if (student != null) // decide instance or template
            {
                res = res.Where(it => it.StudentID
                                   == student.SysUserID);
            }
            if (course != null && course.CourseOfferingID != 0)
            {
                res = res.Where(it => it.CourseOfferingID
                                   == course.CourseOfferingID);
            }

            if (assessment != null)
            {
                if (assessment.AssessmentID != 0)
                {
                    res = res.Where(it => it.AssessmentID
                                       == assessment.AssessmentID);
                }
                if (assessment.Name != null)
                {
                    res = res.Where(it => it.Name
                                       == assessment.Name);
                }
                if (assessment.Type != null)
                {
                    res = res.Where(it => it.Type
                                       == assessment.Type);
                }
                if (assessment.EndDate != null)
                {
                    res = res.Where(it => it.EndDate
                                       == assessment.EndDate);
                }
            }

            return res.ToList();
        }
    }
}

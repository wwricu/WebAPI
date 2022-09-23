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
        public List<Assessment> Query(Assessment template,
                                      SysUser student,
                                      CourseOffering course)
        {
            if (student != null && course != null)
            {
                throw new Exception("Internal Error");
            }
            else if (student != null) // instance
            {
                return db.Queryable<Assessment>()
                         .Where(it => it.StudentID
                                   == student.SysUserID).ToList();
            }
            else if (course != null) // template
            {
                return db.Queryable<Assessment>()
                          .Where(it => it.CourseOfferingID
                                    == course.CourseOfferingID).ToList();
            }

            return new List<Assessment>();
        }

        // delete template/instance assessment
        public void Delete(Assessment assessment)
        {

        }
    }
}

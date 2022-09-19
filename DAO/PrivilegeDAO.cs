using SqlSugar;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class PrivilegeDAO
    {
        private SqlSugarClient db;
        public PrivilegeDAO()
        {
            db = UtilService.GetDBClient();
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(PrivilegeDAO));
        }
        public List<CourseOffering> Query(CourseOffering Course,
                                          int AssessmentID,
                                          string PrivilegeName,
                                          string StudentNumber)
        {
            var res = db.Queryable<CourseOffering>();

            if (Course.CourseName != null)
                res = res.Where(it => it.CourseName.Contains(Course.CourseName));
            if (Course.Year != null)
                res = res.Where(it => it.Year == Course.Year);
            if (Course.Year != null)
                res = res.Where(it => it.Semester == Course.Semester);
            if (AssessmentID > 0)
            {
                res = res.Includes(x => x.AssessmentList)
                         .Where(x => x.AssessmentList
                                      .Any(z => z.AssessmentID == AssessmentID));
            }
            if (PrivilegeName != null)
            {
                res = res.Includes(x => x.PrivilegeList)
                         .Where(x => x.PrivilegeList
                                      .Any(z => z.PrivilegeName == PrivilegeName));
            }
            if (StudentNumber != null)
            {
                res = res.Includes(x => x.StudentList)
                         .Where(x => x.StudentList
                                      .Any(z => z.UserNumber == StudentNumber));
            }

            return res.ToList();
        }
    }
}

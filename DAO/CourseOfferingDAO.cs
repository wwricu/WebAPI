using SqlSugar;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class CourseOfferingDAO
    {
        private SqlSugarClient db;
        public CourseOfferingDAO()
        {
            db = UtilService.GetDBClient();
        }

        public int Insert(CourseOffering Course)
        {
            if (db.Queryable<CourseOffering>()
                  .Where(it => it.CourseName == Course.CourseName)
                  .Where(it => it.Year == Course.Year)
                  .Where(it => it.Semester == Course.Semester)
                  .ToList().Count() != 0)
            {
                throw (new Exception("The course offering has existed!"));
            }
            return db.Insertable(Course).ExecuteCommand();
        }

        public bool Update(CourseOffering Course)
        {
            return db.Updateable(Course)
                     .IgnoreColumns(ignoreAllNullColumns: true)
                     .ExecuteCommandHasChange();
        }
        public List<CourseOffering> Query(CourseOffering Course)
        {
            var res = db.Queryable<CourseOffering>();

            if (Course.CourseName != null)
                res = res.Where(it => it.CourseName.Contains(Course.CourseName));
            if (Course.Year != null)
                res = res.Where(it => it.Year == Course.Year);
            if (Course.Semester != null)
                res = res.Where(it => it.Semester == Course.Semester);

            return res.ToList();
        }
        public List<CourseOffering> Query(CourseOffering Course,
                                  int AssessmentID,
                                  string StaffNumber,
                                  string StudentNumber)
        {
            var res = db.Queryable<CourseOffering>();

            if (Course.CourseName != null)
                res = res.Where(it => it.CourseName.Contains(Course.CourseName));
            if (Course.Year != null)
                res = res.Where(it => it.Year == Course.Year);
            if (Course.Semester != null)
                res = res.Where(it => it.Semester == Course.Semester);
            if (AssessmentID > 0)
            {
                res = res.Includes(x => x.AssessmentList)
                         .Where(x => x.AssessmentList
                                      .Any(z => z.AssessmentID == AssessmentID));
            }
            if (StaffNumber != null)
            {
                res = res.Includes(x => x.StaffList)
                         .Where(x => x.StaffList
                                      .Any(z => z.UserNumber == StaffNumber));
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

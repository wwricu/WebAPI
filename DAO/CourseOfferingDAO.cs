using SqlSugar;
using System.Diagnostics;
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

        public void Delete(CourseOffering course)
        {
            if (course.CourseOfferingID == 0)
            {
                course.CourseOfferingID = db.Queryable<CourseOffering>()
                  .Where(it => it.CourseName == course.CourseName)
                  .Where(it => it.Year == course.Year)
                  .Where(it => it.Semester == course.Semester)
                  .First().CourseOfferingID;
            }

            db.DeleteNav<CourseOffering>(x => x.CourseOfferingID
                                                  == course.CourseOfferingID)
                                        .Include(x => x.StudentList,
                                                    new DeleteNavOptions()
                                                    {
                                                        ManyToManyIsDeleteA = true
                                                    })
                                        .Include(x => x.StudentList,
                                                    new DeleteNavOptions()
                                                    {
                                                        ManyToManyIsDeleteA = true
                                                    })
                                        .ExecuteCommand();
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

            if (Course.CourseOfferingID != 0)
            {
                res = res.Where(it => it.CourseOfferingID == Course.CourseOfferingID);
            }
            if (Course.CourseName != null)
                res = res.Where(it => it.CourseName.Contains(Course.CourseName));
            if (Course.Year != null)
                res = res.Where(it => it.Year == Course.Year);
            if (Course.Semester != null)
                res = res.Where(it => it.Semester == Course.Semester);

            return res.ToList();
        }
        public List<CourseOffering> QueryMultiple(List<string> years,
            List<string> semesters, List<string> names)
        {
            var res = db.Queryable<CourseOffering>();

            if (years != null && years.Count > 0)
            {
                var exp = Expressionable.Create<CourseOffering>();
                foreach (string year in years)
                {
                    exp.OrIF(year != null, it => it.Year == year);
                }
                res = res.Where(exp.ToExpression());
            }

            if (semesters != null && semesters.Count > 0)
            {
                var exp = Expressionable.Create<CourseOffering>();
                foreach (string semester in semesters)
                {
                    exp.OrIF(semester != null, it => it.Semester == semester);
                }
                res = res.Where(exp.ToExpression());
            }

            if (names != null && names.Count > 0)
            {
                var exp = Expressionable.Create<CourseOffering>();
                foreach (string name in names)
                {
                    exp.OrIF(name != null, it => it.CourseName.Contains(name));
                }
                res = res.Where(exp.ToExpression());
            }

            return res.ToList();
        }

        public List<CourseOffering> Query(CourseOffering? Course,
                                          SysUser? user,
                                          Assessment? assessment,
                                          bool contain)
        {
            var res = db.Queryable<CourseOffering>();

            if (Course != null)
            {
                if (Course.CourseOfferingID != 0)
                {
                    Debug.WriteLine("search course by id");
                    res = res.Where(it => it.CourseOfferingID
                                    == Course.CourseOfferingID);
                }
                if (Course.CourseName != null)
                {
                    Debug.WriteLine("search course by name");
                    res = res.Where(it => it.CourseName.Contains(Course.CourseName));
                }
                if (Course.Year != null)
                {
                    Debug.WriteLine("search course by year");
                    res = res.Where(it => it.Year == Course.Year);
                }
                if (Course.Semester != null)
                {
                    Debug.WriteLine("search course by semester");
                    res = res.Where(it => it.Semester == Course.Semester);
                }
            }
            if (assessment != null && assessment.AssessmentID != null)
            {
                Debug.WriteLine("search course by assessement");
                res = res.Includes(x => x.AssessmentList)
                         .Where(x => x.AssessmentList
                         .Any(z => z.AssessmentID
                             == assessment.AssessmentID));
            }
            if (user != null)
            {
                if (user.Permission > 1)
                {
                    if (user.UserNumber != null)
                    {
                        if (contain)
                        {
                            Debug.WriteLine("contains course by staff Number");
                            res = res.Includes(x => x.StaffList)
                                     .Where(x => x.StaffList
                                     .Any(z => z.UserNumber
                                          == user.UserNumber));
                        }
                        else
                        {
                            Debug.WriteLine("not contains course by staff Number");
                            res = res.Includes(x => x.StaffList)
                                     .Where(x => x.StaffList.Count() == 0
                                              || x.StaffList
                                                  .Any(z => z.UserNumber
                                                         != user.UserNumber));

                        }
                    }

                    if (user.UserName != null && contain)
                    {
                        Debug.WriteLine("search course by staff name");
                        res = res.Includes(x => x.StaffList)
                                 .Where(x => x.StaffList
                                 .Any(z => z.UserName
                                            .Contains(user.UserName)));
                    }

                }
                else
                {
                    if (user.UserNumber != null)
                    {
                        Debug.WriteLine("search course by student Number");
                        if (contain)
                            res = res.Includes(x => x.StudentList)
                                                 .Where(x => x.StudentList
                                                 .Any(z => z.UserNumber
                                                      == user.UserNumber));
                        else
                            res = res.Includes(x => x.StudentList)
                                     .Where(x => x.StudentList.Count() == 0
                                              || x.StudentList
                                                  .Any(z => z.UserNumber
                                                         != user.UserNumber));
                    }

                    if (user.UserName != null && contain)
                    {
                        Debug.WriteLine("search course by student name");
                        res = res.Includes(x => x.StudentList)
                                     .Where(x => x.StudentList
                                     .Any(z => z.UserName == user.UserName));
                    }
                }
            }

            return res.ToList();
        }
    }
}
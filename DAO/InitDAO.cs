using SqlSugar;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class InitDAO
    {
        private SqlSugarClient db;
        public InitDAO()
        {
            db = UtilService.GetDBClient();
        }

        public void InitDatabase()
        {
            Debug.WriteLine("init database");
            db.DbMaintenance.CreateDatabase();

            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(SysUser));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(CourseOffering));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(Assessment));

            db.CodeFirst.SetStringDefaultLength(200)
            .InitTables(typeof(StaffOfferingMapping));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(StudentOfferingMapping));

            if (db.Queryable<CourseOffering>().ToList().Count() == 0)
            {
                GenerateStaticData();
            }
        }

        // Generate CourseOffering, Assessment and Location
        public void GenerateStaticData()
        {
            // casacade insert do not insert concrete data but only create relations
            var result = db.InsertNav(OfferingList[0])
                           .Include(it => it.AssessmentList,
                                         new InsertNavOptions()
                                         {OneToManyIfExistsNoInsert = true})
                     .ExecuteCommand();

            result = db.InsertNav(OfferingList[1])
                       .Include(it => it.AssessmentList,
                             new InsertNavOptions()
                             { OneToManyIfExistsNoInsert = true })
                       .ExecuteCommand();

            /*int CoursesID = db.Queryable<CourseOffering>()
                            .Where(it => it.CourseName == "INFO6090")
                            .Select(it => it.CourseOfferingID)
                            .First();
            AssessmentList1[0].CourseOfferingID = CoursesID;
            AssessmentList1[1].CourseOfferingID = CoursesID;
            result = db.InsertNav(new List<Assessment>(AssessmentList1))
                       .Include(it => it.CourseOffering)
                       .ExecuteCommand();

            CoursesID = db.Queryable<CourseOffering>()
                            .Where(it => it.CourseName == "INFT6304")
                            .Select(it => it.CourseOfferingID)
                            .First();
            AssessmentList2[0].CourseOfferingID = CoursesID;
            AssessmentList2[1].CourseOfferingID = CoursesID;
            result = db.InsertNav(new List<Assessment>(AssessmentList2))
                       .Include(it => it.CourseOffering)
                       .ExecuteCommand();

            List<CourseOffering> List6304 = db.Queryable<CourseOffering>()
                             .Where(x => x.AssessmentList
                                       .Any(y => y.CourseOfferingID == CoursesID))
                             .ToList(); // pick one condition from the AssessmentList
            List<Assessment> ListAss = db.Queryable<Assessment>()
                                         .Where(x => x.CourseOfferingID == CoursesID)
                                         .ToList(); // 6304 Assessments
            Debug.WriteLine(ListAss[0].Name);
            Debug.WriteLine(ListAss[1].Name);*/
        }

        private static readonly CourseOffering[] OfferingList =
        {
            new CourseOffering()
            {
                CourseName = "INFO6090",
                Year = "2021",
                Semester = "Trimester 3"
            },
            new CourseOffering()
            {
                CourseName = "INFT6304",
                Year = "2021",
                Semester = "Trimester 2"
            },
        };

        private static readonly Assessment[] AssessmentList1 =
        {
            new Assessment()
            {
                Name = "INFT6090 Assessment 1",
                Type = "quiz",
            },
            new Assessment()
            {
                Name = "INFT6090 Assessment 2",
                Type = "formal",
            },
        };
        private static readonly Assessment[] AssessmentList2 =
        {
            new Assessment()
            {
                Name = "INFT6304 Assessment 1",
                Type = "submit",
            },
            new Assessment()
            {
                Name = "INFT6304 Assessment 2",
                Type = "formal",
            },
        };
    }
}

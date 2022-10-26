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
                        .InitTables(typeof(AssessmentTemplate));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(AssessmentInstance));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(Location));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(Application));
            
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(StaffOfferingMapping));
            db.CodeFirst.SetStringDefaultLength(200)
                        .InitTables(typeof(StudentOfferingMapping));


            if (db.Queryable<CourseOffering>().ToList().Count() == 0)
            {
                db.Insertable(OfferingList).ExecuteCommand();
            }
            if (db.Queryable<Location>().ToList().Count() == 0)
            {
                db.Insertable(LocationList).ExecuteCommand();
            }
        }

        // Generate CourseOffering and Location
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
            new CourseOffering()
            {
                CourseName = "SENG6260",
                Year = "2022",
                Semester = "Semester 2"
            },
        };

        private static readonly Location[] LocationList =
        {
            new Location()
            {
                Campus = "Online",
                Building = "Online",
                Room = "Online"
            },
            new Location()
            {
                Campus = "Callaghan",
                Building = "VG",
                Room = "01"
            },
            new Location()
            {
                Campus = "Callaghan",
                Building = "ES",
                Room = "305"
            },
            new Location()
            {
                Campus = "Callaghan",
                Building = "P",
                Room = "102"
            },
            new Location()
            {
                Campus = "Callaghan",
                Building = "Library",
                Room = "325"
            },
            new Location()
            {
                Campus = "City",
                Building = "Library",
                Room = "202"
            },
            new Location()
            {
                Campus = "City",
                Building = "Main Building",
                Room = "212"
            }
        };
    }
}

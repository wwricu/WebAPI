/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 20/09/2022
******************************************/

using SqlSugar;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class RelationDAO
    {
        private SqlSugarClient db;
        public RelationDAO()
        {
            db = UtilService.GetDBClient();
        }

        public void Insert(SysUser User, List<CourseOffering> courseOfferings)
        {
            if (courseOfferings == null || courseOfferings.Count == 0) return;

            if (User.Permission == 1)
            {
                var list = new List<StudentOfferingMapping>();
                for (int i = 0; i < courseOfferings.Count; i++)
                {
                    list.Add(new StudentOfferingMapping()
                    {
                        SysUserID = User.SysUserID,
                        CourseOfferingID = courseOfferings[i].CourseOfferingID,
                    });
                }
                db.Insertable(list).ExecuteCommand();
                /*var userList = new List<SysUser>
                {
                    User
                };
                db.UpdateNav(userList).Include(z1 => z1.CourseOfferingList)
                    .ExecuteCommand();*/
            }
            else
            {
                var list = new List<StaffOfferingMapping>();
                for (int i = 0; i < courseOfferings.Count; i++)
                {
                    list.Add(new StaffOfferingMapping()
                    {
                        SysUserID = User.SysUserID,
                        CourseOfferingID = courseOfferings[i].CourseOfferingID,
                    });
                }
                db.Insertable(list).ExecuteCommand();
            }
        }

        public void Insert(CourseOffering courseOffering,
                           List<SysUser> users)
        {
            var studentList = new List<StudentOfferingMapping>();
            var staffList = new List<StaffOfferingMapping>();

            if (users == null) return;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Permission == 1)
                {
                    studentList.Add(new StudentOfferingMapping()
                    {
                        SysUserID = users[i].SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });
                }
                else
                {
                    staffList.Add(new StaffOfferingMapping()
                    {
                        SysUserID = users[i].SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });
                }
            }
            db.Insertable(studentList).ExecuteCommand();
            db.Insertable(staffList).ExecuteCommand();
        }

        public void Delete(SysUser user, List<CourseOffering> courseOfferings)
        {
            if (courseOfferings == null || courseOfferings.Count == 0) return;
            var stfList = new List<StaffOfferingMapping>();
            var stdList = new List<StudentOfferingMapping>();

            foreach (var courseOffering in courseOfferings)
            {
                if (user.Permission == 1)
                {
                    stdList.Add(new StudentOfferingMapping()
                    {
                        SysUserID = user.SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });
                }
                else
                {
                    stfList.Add(new StaffOfferingMapping()
                    {
                        SysUserID = user.SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });
                }

            }
            if (user.Permission == 1)
            {
                db.Deleteable(stdList).ExecuteCommand();
            }
            else
            {
                db.Deleteable(stdList).ExecuteCommand();
            }
        }

        public void Delete(CourseOffering courseOffering, List<SysUser> users)
        {
            if (users == null) return;
            var staffList = new List<StaffOfferingMapping>();
            var studentList = new List<StudentOfferingMapping>();

            foreach (var user in users)
            {
                if (user.Permission == 1)
                {
                    studentList.Add(new StudentOfferingMapping()
                    {
                        SysUserID = user.SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });

                }
                else
                {
                    staffList.Add(new StaffOfferingMapping()
                    {
                        SysUserID = user.SysUserID,
                        CourseOfferingID = courseOffering.CourseOfferingID,
                    });
                }
            }

            if (staffList.Count > 0)
                db.Deleteable(staffList).ExecuteCommand();
            if (studentList.Count > 0)
                db.Deleteable(studentList).ExecuteCommand();
        }
    }
}

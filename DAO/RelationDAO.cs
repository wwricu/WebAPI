using SqlSugar;
using System.Collections;
using System.Collections.Generic;
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

        public void RelatePrivilegeStaff(Privilege Privilege, SysUser Staff)
        {
            db.Insertable(new PrivilegeStaffMapping()
            {
                PrivilegeID = Privilege.PrivilegeID,
                SysUserID = Staff.SysUserID,
            });
        }
        public void RelatePrivilegeCourse(Privilege Privilege,
                                          CourseOffering Course)
        {
            db.Insertable(new PrivilegeOfferingMapping()
            {
                PrivilegeID = Privilege.PrivilegeID,
                CourseOfferingID = Course.CourseOfferingID,
            });
        }

        public void RelateStudentCourse(SysUser Student, CourseOffering Course)
        {
            db.Insertable(new StudentOfferingMapping()
            {
                SysUserID = Student.SysUserID,
                CourseOfferingID = Course.CourseOfferingID,
            });
        }
    }
}

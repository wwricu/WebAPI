using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class RelationService
    {
        public static void UpdateRelation(SysUser Staff,
                                          List<CourseOffering> CourseAddList,
                                          List<CourseOffering> CourseRemoveList)
        {
            Staff.SysUserID = new UserDAO().QueryUserByNumber(Staff.UserNumber)[0].SysUserID;
            RelationDAO relationDAO = new();
            relationDAO.Delete(Staff, CourseRemoveList);
            relationDAO.Insert(Staff, CourseAddList);
        }

        public static void UpdateRelation(CourseOffering courseOffering,
                                  List<SysUser> StaffAddList,
                                  List<SysUser> StaffRemoveList)
        {
            RelationDAO relationDAO = new();
            UserDAO userDAO = new();
            if (StaffRemoveList != null)
            {
                for (int i = 0; i < StaffRemoveList.Count; i++)
                {
                    StaffRemoveList[i].SysUserID = userDAO.QueryUserByNumber(
                                                StaffRemoveList[i].UserNumber)[0].SysUserID;
                }
                relationDAO.Delete(courseOffering, StaffRemoveList);
            }
            if (StaffAddList != null)
            {
                for (int i = 0; i < StaffAddList.Count; i++)
                {
                    StaffAddList[i].SysUserID = userDAO.QueryUserByNumber(
                                                StaffAddList[i].UserNumber)[0].SysUserID;
                }
                relationDAO.Insert(courseOffering, StaffAddList);
            }
        }
    }
}

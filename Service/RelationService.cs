using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class RelationService
    {
        public static void UpdateRelation(SysUser user,
                                          List<CourseOffering> CourseAddList,
                                          List<CourseOffering> CourseRemoveList)
        {
            user.SysUserID = new UserDAO().QueryUserByNumber(user.UserNumber)[0].SysUserID;
            RelationDAO relationDAO = new();
            relationDAO.Delete(user, CourseRemoveList);
            relationDAO.Insert(user, CourseAddList);
        }

        public static void UpdateRelation(CourseOffering courseOffering,
                                  List<SysUser> userAddList,
                                  List<SysUser> userRemoveList)
        {
            RelationDAO relationDAO = new();
            UserDAO userDAO = new();
            if (userRemoveList != null)
            {
                for (int i = 0; i < userRemoveList.Count; i++)
                {
                    userRemoveList[i].SysUserID = userDAO.QueryUserByNumber(
                                                userRemoveList[i].UserNumber)[0].SysUserID;
                }
                relationDAO.Delete(courseOffering, userRemoveList);
            }
            if (userAddList != null)
            {
                for (int i = 0; i < userAddList.Count; i++)
                {
                    userAddList[i].SysUserID = userDAO.QueryUserByNumber(
                                                userAddList[i].UserNumber)[0].SysUserID;
                }
                relationDAO.Insert(courseOffering, userAddList);
            }
        }
    }
}

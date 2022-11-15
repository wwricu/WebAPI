/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 21/09/2022
******************************************/

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
            user = new UserDAO().QueryUserByNumber(user.UserNumber).First();
            RelationDAO relationDAO = new();
            relationDAO.Delete(user, CourseRemoveList);
            relationDAO.Insert(user, CourseAddList);

            if (user.Permission != 1) return;

            if (CourseRemoveList != null)
            {
                foreach (var course in CourseRemoveList)
                {
                    AssessmentService.Detach(course, user);
                }
            }
            if (CourseAddList != null)
            {
                foreach (var course in CourseAddList)
                {
                    AssessmentService.Attach(course, user);
                }
            }
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
                    userRemoveList[i] = userDAO.QueryUserByNumber(
                                                userRemoveList[i].UserNumber)[0];
                    if (userRemoveList[i].Permission == 1)
                    {
                        AssessmentService.Detach(courseOffering, userRemoveList[i]);
                    }
                }
                relationDAO.Delete(courseOffering, userRemoveList);
            }

            if (userAddList != null)
            {
                for (int i = 0; i < userAddList.Count; i++)
                {
                    userAddList[i] = userDAO.QueryUserByNumber(
                                                userAddList[i].UserNumber)[0];
                    if (userAddList[i].Permission == 1)
                    {
                        AssessmentService.Attach(courseOffering, userAddList[i]);
                    }
                }
                relationDAO.Insert(courseOffering, userAddList);
            }
        }
    }
}

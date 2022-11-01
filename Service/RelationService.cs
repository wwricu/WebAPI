using System.Diagnostics;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class RelationService
    {
        private RelationService()
        {
            UserDAO = new();
            CourseOfferingDAO = new();
            RelationDAO = new();
            AssessmentService = AssessmentService.GetInstance();
        }
        private static RelationService Instance = new();
        public static RelationService GetInstance()
        {
            Instance ??= new RelationService();
            return Instance;
        }

        private readonly UserDAO UserDAO;
        private readonly CourseOfferingDAO CourseOfferingDAO;
        private readonly RelationDAO RelationDAO;
        private readonly AssessmentService AssessmentService;

        public void UpdateRelation(SysUser user,
                                          List<CourseOffering> CourseAddList,
                                          List<CourseOffering> CourseRemoveList)
        {
            user = UserDAO.QueryUserByNumber(user.UserNumber).First();
            RelationDAO.Delete(user, CourseRemoveList);
            RelationDAO.Insert(user, CourseAddList);

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

        public void UpdateRelation(CourseOffering courseOffering,
                                  List<SysUser> userAddList,
                                  List<SysUser> userRemoveList)
        {
            if (userRemoveList != null)
            {
                for (int i = 0; i < userRemoveList.Count; i++)
                {
                    userRemoveList[i] = UserDAO.QueryUserByNumber(
                                                userRemoveList[i].UserNumber)[0];
                    if (userRemoveList[i].Permission == 1)
                    {
                        AssessmentService.Detach(courseOffering, userRemoveList[i]);
                    }
                }
                RelationDAO.Delete(courseOffering, userRemoveList);
            }

            if (userAddList != null)
            {
                for (int i = 0; i < userAddList.Count; i++)
                {
                    userAddList[i] = UserDAO.QueryUserByNumber(
                                                userAddList[i].UserNumber)[0];
                    if (userAddList[i].Permission == 1)
                    {
                        AssessmentService.Attach(courseOffering, userAddList[i]);
                    }
                }
                RelationDAO.Insert(courseOffering, userAddList);
            }
        }
    }
}

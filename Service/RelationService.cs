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
            user = new UserDAO().QueryUserByNumber(user.UserNumber)[0];
            RelationDAO relationDAO = new();
            relationDAO.Delete(user, CourseRemoveList);
            relationDAO.Insert(user, CourseAddList);

            if (user.Permission != 1) return;
            
            var assessmentDAO = new AssessmentDAO();
            // detach assessments from removed courses
            var assessmentList = new List<Assessment>();
            foreach (var course in CourseRemoveList)
            {
                assessmentList.AddRange(assessmentDAO.Query(
                    new Assessment(), user, course));
            }
            AssessmentService.Delete(assessmentList);
            // attach assessments from added courses
            assessmentList.Clear();
            foreach (var course in CourseAddList)
            {
                var templates = assessmentDAO.Query(new Assessment(),
                    new SysUser(), course);
                foreach (var template in templates)
                {
                    assessmentList.Add(new Assessment(template, user));
                }
            }
            assessmentDAO.Insert(assessmentList);
        }

        public static void UpdateRelation(CourseOffering courseOffering,
                                  List<SysUser> userAddList,
                                  List<SysUser> userRemoveList)
        {
            RelationDAO relationDAO = new();
            UserDAO userDAO = new();
            AssessmentDAO assessmentDAO = new();

            var assessmentList = new List<Assessment>();

            if (userRemoveList != null)
            {
                for (int i = 0; i < userRemoveList.Count; i++)
                {
                    userRemoveList[i] = userDAO.QueryUserByNumber(
                                                userRemoveList[i].UserNumber)[0];
                    assessmentList.AddRange(assessmentDAO.Query(
                                            new Assessment(),
                                            userRemoveList[i],
                                            courseOffering));
                }
                assessmentDAO.Delete(assessmentList);
                relationDAO.Delete(courseOffering, userRemoveList);
            }

            if (userAddList != null)
            {
                var templateList = assessmentDAO.Query(new Assessment(),
                        new SysUser(), courseOffering);
                assessmentList.Clear();

                for (int i = 0; i < userAddList.Count; i++)
                {
                    userAddList[i] = userDAO.QueryUserByNumber(
                                                userAddList[i].UserNumber)[0];
                    if (userAddList[i].Permission == 1)
                    {
                        foreach (var template in templateList)
                        {
                            assessmentList.Add(new Assessment(template, userAddList[i]));
                        }

                    }
                }
                relationDAO.Insert(courseOffering, userAddList);
                assessmentDAO.Insert(assessmentList);
            }
        }
    }
}

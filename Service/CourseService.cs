using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class CourseService
    {
        static public List<CourseOffering> Query(CourseOffering Course,
                                                 SysUser user,
                                                 Assessment assessment)
        {
            /*return new CourseOfferingDAO().TestQuery();*/
            return new CourseOfferingDAO().Query(Course, user, assessment);
        }
    }
}

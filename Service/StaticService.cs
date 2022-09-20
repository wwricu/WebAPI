using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class StaticService
    {
        static public List<CourseOffering> Query(CourseOffering Course)
        {
            return new CourseOfferingDAO().Query(Course);
        }
    }
}

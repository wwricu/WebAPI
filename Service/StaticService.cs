/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 20/09/2022
******************************************/

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

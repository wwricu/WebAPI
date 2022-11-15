/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 21/09/2022
******************************************/

namespace WebAPI.Model
{
    public class CourseQueryModel
    {
        public List<string>? Years { get; set; }
        public List<string>? Semesters { get; set; }
        public List<string>? Names { get; set; }
    }
}

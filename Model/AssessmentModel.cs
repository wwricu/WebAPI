/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 23/09/2022
******************************************/

using WebAPI.Entity;

namespace WebAPI.Model
{
    public class AssessmentModel
    {
        public SysUser? Student { get; set; } 
        public CourseOffering? CourseOffering { get; set; }
        public Assessment? Assessment { get; set; }
    }
}

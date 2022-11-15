/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 21/09/2022
******************************************/

using WebAPI.Entity;

namespace WebAPI.Model
{
    public class RelationModel
    {
        public SysUser? User { get; set; }
        public int permission { get; set; }
        public List<CourseOffering>? CourseAddList { get; set; }
        public List<CourseOffering>? CourseRemoveList { get; set; }
        public CourseOffering? CourseOffering { get; set; }
        public List<SysUser>? UserAddList { get; set; }
        public List<SysUser>? UserRemoveList { get; set; }
    }
}

/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Jiaming Ta
  Date   : 15/09/2022
******************************************/

using SqlSugar;

namespace WebAPI.Entity
{
    public class StudentOfferingMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int SysUserID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseOfferingID { get; set; }
    }
}

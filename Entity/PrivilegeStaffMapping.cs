/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 15/09/2022
******************************************/

using SqlSugar;

namespace WebAPI.Entity
{
    public class PrivilegeStaffMapping
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int PrivilegeID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int SysUserID { get; set; }
    }
}

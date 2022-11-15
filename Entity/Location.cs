/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Jiaming Ta
  Date   : 15/09/2022
******************************************/

using SqlSugar;

namespace WebAPI.Entity
{
    public class Location
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int LocationID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Room { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Building { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string? Campus { get; set; }
    }
}

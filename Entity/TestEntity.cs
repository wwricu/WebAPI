/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Jiaming Ta
  Date   : 15/09/2022
******************************************/

using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    [SugarTable("TestEntity")]
    public class TestEntity
    {
        public TestEntity()
        {
            Id = 1;
            Name = "testname";
            Age = 1;
        }
        
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true, ColumnName = "testEntityID")]
        public int Id { get; set; }
        [SugarColumn(ColumnDataType = "varchar(64)")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class ColumnTest
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public string[] Address { get; set; }
    }
}

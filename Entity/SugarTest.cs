using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class StudentA
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public int SchoolId { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(SchoolId))]//一对一
        public SchoolA? SchoolA { get; set; }

    }
    public class SchoolA
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }
    }
}

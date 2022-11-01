using SqlSugar;
using System.Security.Principal;

namespace WebAPI.Entity
{
    public class Document
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int DocumentID { get; set; }
        public long ApplicationID { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        [SugarColumn(ColumnDataType = "varchar(8000)")]
        public string? Url { get; set; }
    }
}

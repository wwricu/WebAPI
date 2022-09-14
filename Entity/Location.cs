using SqlSugar;

namespace WebAPI.Entity
{
    public class Location
    {
        public Location()
        {
            Room = "Default Room";
            Building = "Default Building";
            Campus = "Default Campus";
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int LocationID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Room { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Building { get; set; }
        [SugarColumn(ColumnDataType = "varchar(100)")]
        public string Campus { get; set; }
    }
}

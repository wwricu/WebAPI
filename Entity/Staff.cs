using SqlSugar;

namespace WebAPI.Entity
{
    public class Staff
    {
        public string? StaffNumber { get; set; }
        public string? StaffName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Faculty { get; set; }
        public int Status { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
                  nameof(PrivilegeStaffMapping.StaffID),
                  nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }

}

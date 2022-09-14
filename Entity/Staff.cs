using Microsoft.AspNetCore.Identity;
using SqlSugar;

namespace WebAPI.Entity
{
    public class Staff : IdentityUser
    {
        [PersonalData]
        public string? StaffNumber { get; set; }
        [PersonalData]
        public string? StaffName { get; set; }
        [PersonalData]
        public DateTime Birthdate { get; set; }
        [PersonalData]
        public string? AddressLine1 { get; set; }
        [PersonalData]
        public string? AddressLine2 { get; set; }
        [PersonalData]
        public string? AddressLine3 { get; set; }
        [PersonalData]
        public string? Faculty { get; set; }
        [PersonalData]
        public int Status { get; set; }

        [Navigate(typeof(PrivilegeStaffMapping),
                  nameof(PrivilegeStaffMapping.StaffID),
                  nameof(PrivilegeStaffMapping.PrivilegeID))]
        public List<Privilege>? PrivilegeList { get; set; }
    }

}

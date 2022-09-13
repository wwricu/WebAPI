using Microsoft.AspNetCore.Identity;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entity
{
    public class TestUser : IdentityUser
    {
        [Required]
        [StringLength(128)]
        [SugarColumn(ColumnDataType = "varchar(64)")]
        public string? Address { get; set; }
    }
}

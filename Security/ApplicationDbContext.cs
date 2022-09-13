using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entity;

/*
dotnet tool install --global dotnet-ef
dotnet ef migrations add AppDbContext_Initial
dotnet ef database update
 */
namespace WebAPI.Security
{
    public class ApplicationDbContext : IdentityDbContext<TestUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

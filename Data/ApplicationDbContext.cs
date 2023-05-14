using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Visitors> Visitors { get; set; } = default!;
        public DbSet<StaffNames> StaffNames { get; set; } = default!;
    }
}
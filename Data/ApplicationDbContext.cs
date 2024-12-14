using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QrCodeGeneratorWebAppMVC.Models;

namespace QrCodeGeneratorWebAppMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<QrCodeGeneratorWebAppMVC.Models.QrCode> QrCode { get; set; } = default!;
    }
}

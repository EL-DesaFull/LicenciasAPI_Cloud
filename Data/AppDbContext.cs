using Microsoft.EntityFrameworkCore;
using LicenciasAPI.Models;

namespace LicenciasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<SolicitudLicencia> Solicitudes { get; set; }
    }
}

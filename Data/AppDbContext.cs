using Microsoft.EntityFrameworkCore;
using RegisterAPI.Models;

namespace RegisterAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserFiesta> UsuariosFiesta => Set<UserFiesta>();
    }
}
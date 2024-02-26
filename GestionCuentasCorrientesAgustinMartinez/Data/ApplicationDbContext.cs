using GestionCuentasCorrientesAgustinMartinez.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCuentasCorrientesAgustinMartinez.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
    }
}

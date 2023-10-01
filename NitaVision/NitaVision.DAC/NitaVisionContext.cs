using Microsoft.EntityFrameworkCore;
using NitaVision.SPI.Entity;
using System.Reflection;

namespace NitaVision.DAC
{
    public class NitaVisionContext : DbContext
    {
        public DbSet<Audio> Audio { get; set; }

        public string DbPath { get; }

        public NitaVisionContext()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DbPath = System.IO.Path.Join(exePath, "NitaVision.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}

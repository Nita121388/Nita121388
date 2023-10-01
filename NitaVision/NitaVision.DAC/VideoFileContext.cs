using Microsoft.EntityFrameworkCore;
using NitaVision.SPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NitaVision.DAC
{
    public class VideoFileContext : DbContext
    {
        public DbSet<VideoFile> VideoFile { get; set; }

        public string DbPath { get; }

        public VideoFileContext()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DbPath = System.IO.Path.Join(exePath, "NitaVision.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}

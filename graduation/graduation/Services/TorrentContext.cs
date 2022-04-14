using System;
using System.IO;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using graduation.Models;

namespace graduation.Services
{
    public class TorrentContext:DbContext
    {
        public DbSet<Torrent> Torrents { get; set; }
        public TorrentContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath =Path.Combine( FileSystem.AppDataDirectory,@"torrents.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}

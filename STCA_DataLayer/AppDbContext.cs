using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace STCA_DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        // this aproach is for use DBContext object without Dependency Injection.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //string dbStringConn = @"Server=(localdb)\mssqllocaldb;Database=STCA_DEMO;Trusted_Connection=True";

        //    //optionsBuilder.UseSqlServer(dbStringConn);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // usando un método extension que he programado para ModelBuilder
            modelBuilder.Seed();
        }

        public DbSet<TipoAreaAcceso> TiposAreasAcceso { get; set; }

    }
}

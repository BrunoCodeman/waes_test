using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace Waes.Core 
{
    public class WaesContext : DbContext 
    {
        public DbSet<Entity> Entities { get; set; }

        public WaesContext(){}
        public WaesContext( DbContextOptions<WaesContext>  dbco): base(dbco){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=waes.db");
            }
        }
    }
}


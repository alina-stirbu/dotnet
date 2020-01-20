using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ApplicationDbContext: DbContext
    {
       
        public ApplicationDbContext() : base()
        {
            
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    connect to sql server database
        //    options.UseSqlServer(this.Configuration.GetConnectionString("WebApiDatabase"));
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=HotelApp;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Rate> Rates { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Database
{
    public class ResaleContext : IdentityDbContext<User>
    {
        public ResaleContext(DbContextOptions<ResaleContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Venue>().ToTable("Venues");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }
}

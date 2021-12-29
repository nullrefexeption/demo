using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MsSql
{
    public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<UserFlight> UserFlights { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<City>(x =>
            {
                x.HasKey(c => c.Id);

                x.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
                
                x.HasIndex(c => c.Name)
                .IsUnique();

                x.HasData(
                    new City[] {
                    new City { Id = 1, Name = "Алматы"},
                    new City { Id = 2, Name = "Нур-Султан"},
                    new City { Id = 3, Name = "Шымкент"},
                    new City { Id = 4, Name = "Караганда"},
                    new City { Id = 5, Name = "Уральск"}
                });
            });

            builder.Entity<Flight>(x =>
            {
                x.HasKey(c => c.Id);

                x.Property(c => c.ArrivalCityId).IsRequired();
                x.Property(c => c.DepartureCityId).IsRequired();
                
                x.Property(c => c.ArrivalTime).IsRequired();
                x.Property(c => c.DepartureTime).IsRequired();

                x.HasOne(c => c.DepartureCity)
                .WithMany(c => c.OutgoingFlights)
                .HasForeignKey(c => c.DepartureCityId)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasOne(c => c.ArrivalCity)
               .WithMany(c => c.IncomingFlights)
               .HasForeignKey(c => c.ArrivalCityId)
               .OnDelete(DeleteBehavior.NoAction);

               // x.HasData(
               //    new Flight[] {
               //     new Flight { 
               //         DepartureCityId = 1, 
               //         ArrivalCityId = 2, 
               //         DepartureTime = new DateTime(2021, 12, 29, 12, 22, 10), 
               //         ArrivalTime = new DateTime(2021, 12, 29, 12, 33, 10)
               //     },
               //     new Flight { 
               //         DepartureCityId = 3, 
               //         ArrivalCityId = 4, 
               //         DepartureTime = new DateTime(2021, 12, 29, 15, 22, 10),
               //         ArrivalTime = new DateTime(2021, 12, 29, 15, 44, 10)
               //     },
                    
               //});
            });

            builder.Entity<UserFlight>(x =>
            {
                x.HasKey(c => new {c.UserId, c.FlightId});

                x.HasOne(c => c.User)
                .WithMany(c => c.UserFlights)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                x.HasOne(c => c.Flight)
               .WithMany(c => c.UserFlights)
               .HasForeignKey(c => c.FlightId)
               .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using StreetFood.Domain.Mapping;
using StreetFood.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetFood.Domain
{
    public class StreetFoodDbContext: DbContext
    {
        public StreetFoodDbContext(DbContextOptions<StreetFoodDbContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new FoodMap());
            builder.ApplyConfiguration(new CountryMap());
            builder.ApplyConfiguration(new CountryFoodMap());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<CountryFood> CountryFood { get; set; }
    }
}

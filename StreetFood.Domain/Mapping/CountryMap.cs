using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreetFood.Domain.Models;

namespace StreetFood.Domain.Mapping
{
    public class CountryMap : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(256)");
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Bangladesh"
                },
                new Country
                {
                    Id = 2,
                    Name = "India"
                },
                new Country
                {
                    Id = 3,
                    Name = "Srilanka"
                },
                new Country
                {
                    Id = 4,
                    Name = "Pakistan"
                },
                new Country
                {
                    Id = 5,
                    Name = "Bhutan"
                },
                new Country
                {
                    Id = 6,
                    Name = "China"
                },
                new Country
                {
                    Id = 7,
                    Name = "Japan"
                },
                new Country
                {
                    Id = 8,
                    Name = "Nepal"
                },
                new Country
                {
                    Id = 9,
                    Name = "Qatar"
                },
                new Country
                {
                    Id = 10,
                    Name = "Singapore"
                },
                new Country
                {
                    Id = 11,
                    Name = "Malaysia"
                },
                new Country
                {
                    Id = 12,
                    Name = "Thailand"
                },
                new Country
                {
                    Id = 13,
                    Name = "Australia"
                },
                new Country
                {
                    Id = 14,
                    Name = "Canada"
                },
                new Country
                {
                    Id = 15,
                    Name = "USA"
                }
                );
        }
    }
}

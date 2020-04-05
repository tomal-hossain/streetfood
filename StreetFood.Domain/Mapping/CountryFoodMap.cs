using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreetFood.Domain.Models;

namespace StreetFood.Domain.Mapping
{
    public class CountryFoodMap : IEntityTypeConfiguration<CountryFood>
    {
        public void Configure(EntityTypeBuilder<CountryFood> builder)
        {
            builder.ToTable("CountryFood");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.FoodId);
            builder.HasIndex(x => x.CountryId);
            builder.Property(x => x.FoodId).IsRequired().HasColumnType("int");
            builder.Property(x => x.CountryId).IsRequired().HasColumnType("int");
            builder.HasOne(foodCountry => foodCountry.Food)
                .WithMany(food => food.PopularIn)
                .HasForeignKey(foodCountry => foodCountry.FoodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

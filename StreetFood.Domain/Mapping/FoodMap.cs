using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreetFood.Domain.Models;

namespace StreetFood.Domain.Mapping
{
    public class FoodMap : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("Food");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.AddedById);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(128)");
            builder.Property(x => x.ImageUrl).HasColumnType("nvarchar(128)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)");
            builder.Property(x => x.AddedAt).HasColumnType("date");
            builder.Property(x => x.AddedById).IsRequired().HasColumnType("int");
            builder.HasOne(userFood => userFood.AddedBy)
                .WithMany(user => user.Foods)
                .HasForeignKey(userFood => userFood.AddedById)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

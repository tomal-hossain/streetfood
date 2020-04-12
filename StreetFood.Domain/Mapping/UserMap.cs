using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreetFood.Domain.Models;

namespace StreetFood.Domain.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(128)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(64)");
            builder.Property(x => x.IsConfirmed).HasDefaultValue(false).HasColumnType("bit");
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(256)");
        }
    }
}

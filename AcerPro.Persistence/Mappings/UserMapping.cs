using AcerPro.Domain.Aggregates;
using AcerPro.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcerPro.Persistence.Mappings;

internal class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Firstname)
            .HasColumnName(nameof(User.Firstname))
            .HasMaxLength(Name.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => Name.Create(c).Value);

        builder.Property(c => c.Lastname)
            .HasColumnName(nameof(User.Lastname))
            .HasMaxLength(Name.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => Name.Create(c).Value);

        builder.Property(c => c.Email)
            .HasColumnName(nameof(User.Email))
            .HasMaxLength(Email.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => (Email)c);

        builder.Property(c => c.Password)
            .HasColumnName(nameof(User.Password))
            .HasMaxLength(Password.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => (Password)c);

        builder.HasIndex(c => c.Email)
            .IsUnique(true);

        builder.HasMany(c => c.TargetApps)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

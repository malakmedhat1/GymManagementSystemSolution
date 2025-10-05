using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class GymUserCongiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        //    public void Configure(EntityTypeBuilder<T> builder)
        //    {
        //        builder.HasKey("id");
        //        builder.Property<DateTime>("createdAt").HasDefaultValueSql("getdate()");
        //        builder.Property<DateTime>("updatedAt").HasDefaultValueSql("getdate()");
        //        builder.Property<string>("firstName").IsRequired().HasMaxLength(50);
        //        builder.Property<string>("lastName").IsRequired().HasMaxLength(50);
        //        builder.Property<string>("email").IsRequired().HasMaxLength(100);
        //        builder.Property<string>("phoneNumber").IsRequired().HasMaxLength(15);
        //        builder.Property<DateTime>("dateOfBirth").IsRequired();
        //        builder.Property< int
        //{

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            
            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            // Add a check constraint to ensure the email contains an "@" symbol and a domain
            // Valid email format:

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmailCheck", "Email LIKE '_%@_%._%'");
                tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' AND Phone NOT LIKE '%[^0-9]%'");
            });

            // unique non clustered index on email

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.OwnsOne(x => x.Address, AddressBuilder =>
            {
                AddressBuilder.Property(a => a.BuildingNumber)
                    .HasColumnName("BuildingNumber")
                    .HasColumnType("int");
                
                AddressBuilder.Property(a => a.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
               
                AddressBuilder.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
            });
        }
    }
}

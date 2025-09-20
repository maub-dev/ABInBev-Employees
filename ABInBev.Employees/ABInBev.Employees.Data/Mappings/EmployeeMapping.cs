using ABInBev.Employees.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABInBev.Employees.Data.Mappings
{
    internal class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.DocumentNumber)
                .IsUnique();
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.DocumentNumber)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(x => x.UserIdentityId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.Phone1)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(x => x.Phone2)
                .IsRequired()
                .HasColumnType("varchar(30)");
        }
    }
}

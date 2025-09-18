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
                .HasColumnType("varchar(30)");

            builder.Property(x => x.UserIdentityId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.HasMany(x => x.Phones)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);
        }
    }
}

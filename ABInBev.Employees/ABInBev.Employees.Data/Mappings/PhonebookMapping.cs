using ABInBev.Employees.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABInBev.Employees.Data.Mappings
{
    internal class PhonebookMapping : IEntityTypeConfiguration<Phonebook>
    {
        public void Configure(EntityTypeBuilder<Phonebook> builder)
        {
            builder.ToTable("Phonebook");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Type)
                .IsRequired()
                .HasColumnType("smallint");
        }
    }
}

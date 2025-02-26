using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    internal class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //Chaves, relacionamentos e index
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Tables)
                .WithMany(m => m.People);

            //Propriedades
            builder.Property(p => p.Name)
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Email)
                .HasColumnType("varchar(200)");
        }
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    internal class TableMapping : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            //Chaves, relacionamentos e index
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.TableCode);

            builder.HasMany(m => m.People)
                .WithMany(p => p.Tables);

            builder.HasMany(m => m.Items)
                .WithOne(p => p.Table)
                .HasForeignKey(i => i.TableId);

            //Propriedades
            builder.Property(m => m.TableCode)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(m => m.Name)
                .HasColumnType("varchar(200)");

            builder.Property(m => m.Couvert)
                .HasColumnType("decimal(10,2)");

            builder.Property(m => m.ServiceFee)
                .HasColumnType("decimal(10,2)");

            builder.Property(m => m.CreatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();
        }
    }
}
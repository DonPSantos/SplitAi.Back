using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    internal class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            //Chaves, relacionamentos e index
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Table)
                .WithMany(m => m.Items)
                .HasForeignKey(i => i.TableId);

            builder.HasMany(i => i.Consumptions)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId);

            //Propriedades
            builder.Property(i => i.Description)
                .HasColumnType("varchar(200)");

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.TableId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();
        }
    }
}
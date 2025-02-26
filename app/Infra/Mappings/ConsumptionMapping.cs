using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class ConsumptionMapping : IEntityTypeConfiguration<Consumption>
    {
        public void Configure(EntityTypeBuilder<Consumption> builder)
        {
            //Chaves, relacionamentos e index
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.Item)
                .WithMany(i => i.Consumptions)
                .HasForeignKey(c => c.ItemId);

            builder.HasMany(c => c.Participants)
                .WithMany(p => p.Consumptions);

            //Propriedades
            builder.Property(p => p.Quantity)
                .HasColumnType("int");
        }
    }
}
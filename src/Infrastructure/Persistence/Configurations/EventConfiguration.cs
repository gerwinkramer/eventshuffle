using Eventshuffle.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventshuffle.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            // This relation uses a backing field to encapsulate the relation (read-only)
            builder.HasMany(e => e.DateOptions)
                .WithOne()
                .HasForeignKey(c => c.EventId)
                .IsRequired()
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

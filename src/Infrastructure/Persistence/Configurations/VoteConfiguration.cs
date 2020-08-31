using Eventshuffle.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventshuffle.Infrastructure.Persistence.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Votes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne<EventEntity>()
                .WithMany()
                .HasForeignKey(p => p.EventId)
                .IsRequired();
        }
    }
}

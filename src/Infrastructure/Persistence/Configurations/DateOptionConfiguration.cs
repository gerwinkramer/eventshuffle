using Eventshuffle.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventshuffle.Infrastructure.Persistence.Configurations
{
    public class DateOptionConfiguration : IEntityTypeConfiguration<DateOption>
    {
        public void Configure(EntityTypeBuilder<DateOption> builder)
        {
            builder.ToTable("DateOptions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EventId)
                .IsRequired();
            
            builder.Property(e => e.Date)
                .HasColumnType("Date")
                .IsRequired();

            builder.HasMany(e => e.Votes)
                .WithOne()
                .HasForeignKey(c => c.DateOptionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); // Needed to prevent cycles.
        }
    }
}

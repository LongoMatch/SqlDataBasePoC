using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlDataBasePoC.Models.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable(DataBaseConstants.TABLE_TEAMS);

            builder.Property(t => t.Name).HasColumnName(DataBaseConstants.TABLE_TEAMS_FIELD_NAME);
            builder.Property(t => t.Name).IsRequired();

            builder.Property(t => t.Country).HasColumnName(DataBaseConstants.TABLE_TEAMS_FIELD_COUNTRY);
            builder.Property(t => t.Country).IsRequired(false);

            // This 1:N relationship is created by convention (adding the fields Players, TeamId and Team)
            // However its also possible to define manually.
            builder
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .IsRequired();
        }
    }
}


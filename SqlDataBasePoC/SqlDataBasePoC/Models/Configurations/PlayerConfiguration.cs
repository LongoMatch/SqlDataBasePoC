using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlDataBasePoC.Models.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable(DataBaseConstants.TABLE_PLAYERS);

            builder.Property(t => t.Name).HasColumnName(DataBaseConstants.TABLE_PLAYERS_FIELD_NAME);
            builder.Property(p => p.Name).IsRequired();

            builder.Property(t => t.Position).HasColumnName(DataBaseConstants.TABLE_PLAYERS_FIELD_POSITION);
            builder.Property(p => p.Position).IsRequired(false);
        }
    }
}


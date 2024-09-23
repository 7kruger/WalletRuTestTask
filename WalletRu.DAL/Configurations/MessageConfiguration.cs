using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletRu.Domain.Entities;

namespace WalletRu.DAL.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Body).HasMaxLength(128);
        builder.Property(x => x.Timestamp).HasMaxLength(64);
        builder.Property(x => x.SerialNumber).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
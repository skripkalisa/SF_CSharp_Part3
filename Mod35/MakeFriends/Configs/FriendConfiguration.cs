using MakeFriends.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeFriends.Configs;

public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
  public void Configure(EntityTypeBuilder<Friend> builder)
  {
    builder.ToTable("UserFriends").HasKey(p => p.Id);
    builder.Property(x => x.Id).UseIdentityColumn();
  }
}
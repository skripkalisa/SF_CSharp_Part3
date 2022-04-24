using EntityFrameworkCore.Repository.Interfaces;

namespace MakeFriends.Models.Users;

public class Friend : IRepository
{
  public int Id { get; set; }
  public string UserId { get; set; } = default!;
  public User? User { get; set; }

  public string CurrentFriendId { get; set; } = default!;

  public User? CurrentFriend { get; set; }
  public void Dispose()
  {
    throw new NotImplementedException();
  }
}
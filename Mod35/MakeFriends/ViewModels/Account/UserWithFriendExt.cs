using MakeFriends.Models.Users;

namespace MakeFriends.ViewModels.Account
{
    public class UserWithFriendExt: User
    {
        public bool IsFriendWithCurrent { get; set; }
    }
}

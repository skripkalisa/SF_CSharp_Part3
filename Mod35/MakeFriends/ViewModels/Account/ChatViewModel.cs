using MakeFriends.Models;
using MakeFriends.Models.Users;

namespace MakeFriends.ViewModels.Account;

public class ChatViewModel
{
    public User You { get; set; } = default! ;

    public User ToWhom { get; set; } = default! ;

    public List<Message> History { get; set; } = default! ;

    public MessageViewModel NewMessage { get; set; }

    public ChatViewModel()
    {
        NewMessage = new MessageViewModel();
    }

}
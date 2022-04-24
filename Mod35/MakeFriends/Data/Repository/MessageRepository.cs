using MakeFriends.Models;
using MakeFriends.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MakeFriends.Data.Repository;

public abstract class MessageRepository : Repository<Message>
{
  protected MessageRepository(ApplicationDbContext db) : base(db)
  {
  }

  public IEnumerable<Message> GetMessages(User sender, User recipient)
  {
    Set.Include(x => x.Recipient);
    Set.Include(x => x.Sender);

    var from = Set.AsEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
    var to = Set.AsEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();

    var messageList = new List<Message>();
    messageList.AddRange(from);
    messageList.AddRange(to);
    var orderedMessageList = messageList.OrderBy(x => x.Id);
    return orderedMessageList.ToList();
  }
}
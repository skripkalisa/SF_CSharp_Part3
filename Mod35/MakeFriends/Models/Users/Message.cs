namespace MakeFriends.Models.Users;

public class Message
{
  public int Id { get; set; } 
  public string Text { get; set; } = default! ;

  public string SenderId { get; set; } = default! ;
  public User Sender { get; set; } = default! ;

  public string RecipientId { get; set; } = default! ;
  public User Recipient { get; set; } = default! ;
}
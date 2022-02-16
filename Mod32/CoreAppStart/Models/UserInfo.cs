using System;

namespace CoreAppStart.Models
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string UserAgent { get; set; }
    }
}
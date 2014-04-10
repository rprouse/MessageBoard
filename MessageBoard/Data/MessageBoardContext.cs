using System.Data.Entity;

namespace MessageBoard.Data
{
    public class MessageBoardContext : DbContext
    {
        public MessageBoardContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }
}
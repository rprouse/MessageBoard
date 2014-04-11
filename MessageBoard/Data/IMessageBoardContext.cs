using System.Data.Entity;

namespace MessageBoard.Data
{
    public interface IMessageBoardContext
    {
        DbSet<Topic> Topics { get; set; }
        DbSet<Reply> Replies { get; set; }
    }
}
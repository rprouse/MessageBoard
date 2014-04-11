using System.Linq;

namespace MessageBoard.Data
{
    public interface IMessageBoardRepository
    {
        IQueryable<Topic> GetTopics();
        IQueryable<Reply> GetRepliesByTopic( int topicId );
    }
}
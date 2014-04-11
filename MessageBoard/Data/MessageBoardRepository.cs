using System.Linq;

namespace MessageBoard.Data
{
    class MessageBoardRepository : IMessageBoardRepository
    {
        private readonly IMessageBoardContext _context;

        public MessageBoardRepository(IMessageBoardContext context)
        {
            _context = context;
        }

        #region Implementation of IMessageBoardRepository

        public IQueryable<Topic> GetTopics()
        {
            return _context.Topics;
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _context.Replies.Where(r => r.TopicId == topicId);
        }

        #endregion
    }
}
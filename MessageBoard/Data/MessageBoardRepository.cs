using System;
using System.Linq;

namespace MessageBoard.Data
{
    class MessageBoardRepository : IMessageBoardRepository
    {
        private readonly MessageBoardContext _context;

        public MessageBoardRepository(MessageBoardContext context)
        {
            _context = context;
        }

        #region Implementation of IMessageBoardRepository

        public IQueryable<Topic> GetTopics()
        {
            return _context.Topics;
        }

        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _context.Topics.Include("Replies");
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _context.Replies.Where(r => r.TopicId == topicId);
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _context.Topics.Add(newTopic);
                return true;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public bool AddReply(Reply newReply)
        {
            try
            {
                _context.Replies.Add(newReply);
                return true;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        #endregion
    }
}
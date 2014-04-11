using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageBoard.Data;

namespace MessageBoard.Controllers
{
    public class RepliesController : ApiController
    {
        private readonly IMessageBoardRepository _repo;

        public RepliesController(IMessageBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Reply> Get(int topicId)
        {
            return _repo.GetRepliesByTopic(topicId)
                .OrderByDescending(t => t.Created);
        }

        public HttpResponseMessage Post(int topicId, [FromBody] Reply newReply)
        {
            newReply.TopicId = topicId;

            if(newReply.Created == default(DateTime))
                newReply.Created = DateTime.UtcNow;

            if(_repo.AddReply(newReply) && _repo.Save())
                return Request.CreateResponse(HttpStatusCode.Created, newReply);

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}

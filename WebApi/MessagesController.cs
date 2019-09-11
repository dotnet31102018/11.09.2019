using _1109.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _1109.Controllers
{
    public class MessagesController : ApiController
    {
        private static List<Message> messages = new List<Message>();
        static MessagesController()
        {
            messages.Add(new Message { Id = 1, Sender = "Danny", Body = "Hello from Danny" });
            messages.Add(new Message { Id = 2, Sender = "Galit", Body = "How are you Danny?" });
            messages.Add(new Message { Id = 3, Sender = "Danny", Body = "I'm good."});
            messages.Add(new Message { Id = 4, Sender = "Steve", Body = "What's up?" });
        }

        // GET api/values
        public List<Message> Get()
        {
            return messages;
        }

        // GET api/values/5
        public Message Get(int id)
        {
            Message result = messages.FirstOrDefault(m => m.Id == id);
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Message result = messages.FirstOrDefault(m => m.Id == id);
            if (result != null)
                messages.Remove(result);

        }
    }
}

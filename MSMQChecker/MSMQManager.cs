using Microsoft.Extensions.Configuration;
using System;
using Experimental.System.Messaging;

namespace MSMQChecker
{
    public class MSMQManager : IDisposable
    {
        private IConfiguration Configuration { get; }
        private MessageQueue messageQueue = null;
        private bool disposed = false;

        public MSMQManager(IConfiguration configuration)
        {
            Configuration = configuration;

            string description = configuration.GetValue<string>("MSMQ:description");
            string path = configuration.GetValue<string>("MSMQ:path");
            try
            {
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
                messageQueue = new MessageQueue(path);
                messageQueue.Label = description;
                messageQueue.MessageReadPropertyFilter.SetAll();
            }
            catch
            {
                throw;
            }
        }

        public GazpromMessenger.Models.Message ReceiveMessage()
        {
            try
            {
                Message myMessage = messageQueue.Receive();
                myMessage.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                GazpromMessenger.Models.Message modelMessage = new GazpromMessenger.Models.Message
                {
                    Description = myMessage.Body.ToString(),
                    CreateTime = myMessage.SentTime
                };
                return modelMessage;
            }
            catch
            {
                throw;
            }
        }

        public int Count()
        {
            int count = 0;
            var enumerator = messageQueue.GetMessageEnumerator2();
            while (enumerator.MoveNext())
                count++;

            return count;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    messageQueue.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

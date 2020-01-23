using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using System;

namespace GazpromMessenger.Helpers
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
            }
            catch
            {
                throw;
            }
        }

        public bool SendMessage(string messageText)
        {
            bool sucess = false;
            try
            {
                messageQueue.Send(messageText);
                sucess = true;
            }
            catch
            {
                //throw;
            }
            return sucess;
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

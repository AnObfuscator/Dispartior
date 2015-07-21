using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BatchSharp.Messaging
{
    public class Channel : IDisposable
    {
        private volatile bool run = true;

        private readonly Thread listenThread;
        private readonly UdpClient socket;
        private readonly ConcurrentDictionary<string, Action<string>> messageHandlers;

        public Channel(int port)
        {
            messageHandlers = new ConcurrentDictionary<string, Action<string>>();
            socket = new UdpClient(port);
            listenThread = new Thread(Listener);
        }

        public void Send(BaseMessage message, IPEndPoint endpoint)
        {
            Console.WriteLine("Sending Message: " + message.GetType().ToString());
            var serializedMessage = message.Serialize();
            socket.Send(serializedMessage, serializedMessage.Length, endpoint);
        }

        public void Listen()
        {
            if (listenThread.IsAlive)
            {
                return;
            }

            listenThread.Start();
        }

        private void Listener()
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (run)
            {
                var rawMessage = socket.Receive(ref endpoint);
                var message = System.Text.Encoding.Default.GetString(rawMessage);
                var jsonStartIndex = message.IndexOf("{");
                var messageType = message.Substring(0, jsonStartIndex);
                var messageJson = message.Substring(jsonStartIndex);

                Console.WriteLine(string.Format("Got message of type {0} : {1}", messageType, messageJson));

                Action<string> handler;
                if (messageHandlers.TryGetValue(messageType, out handler))
                {
                    Console.WriteLine("invoking message handler");
                    handler(messageJson);
                }
            }
        }

        public void Stop()
        {
            run = false;
            listenThread.Join();
        }

        public void AddHandler<T>(Action<string> messageHandler) where T : BaseMessage
        {
            var messageTypeName = typeof(T).Name;
            messageHandlers[messageTypeName] = messageHandler;
            Console.WriteLine("Added handler for " + messageTypeName);
        }

        #region IDisposable implementation
        public void Dispose()
        {
            Stop();
        }
        #endregion
    }
}


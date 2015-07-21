using System;

namespace Dispartior.Messaging.Messages.Requests
{
    public class Heartbeat : BaseMessage
    {
        public enum HeartbeatStatus
        {
            SENT,
            TIMEOUT,
            RECEIVED
        }

        public string UUID { get; set; }
        
        DateTime TimeStamp { get; set; }

        public HeartbeatStatus Status { get; set; } 

        public static Heartbeat CreateNew()
        {
            var heartbeat = new Heartbeat();
            heartbeat.UUID = Guid.NewGuid().ToString();
            heartbeat.TimeStamp = DateTime.UtcNow;
            heartbeat.Status = HeartbeatStatus.SENT;
            return heartbeat;
        }
    }
}


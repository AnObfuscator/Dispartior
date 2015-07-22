using Dispartior.StatusCodes;

namespace Dispartior.Messaging.Messages.Responses
{
    public class ComputationResult : BaseMessage
    {
        public string UUID { get; set; }

        public string WorkerId { get; set; }

        public ResultStatus Status { get; set; }
    }
}


﻿using System;
using System.Collections.Generic;
using Dispartior.StatusCodes;

namespace Dispartior.Messaging.Messages.Responses
{
    public class ComputeStatus : BaseMessage
    {
        public string UUID { get; set; }

        public DateTime Timestamp { get; set; }

        public Dictionary<string, RunnerStatus> Workers { get; set; }
    }
}


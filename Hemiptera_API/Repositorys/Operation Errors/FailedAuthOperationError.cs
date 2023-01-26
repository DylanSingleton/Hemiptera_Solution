﻿using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services.Service_Errors
{
    public class FailedAuthOperationError : OperationError
    {
        public FailedAuthOperationError()
    : base("Email or password incorrect.", HttpStatusCode.Unauthorized)
        {
        }
    }
}
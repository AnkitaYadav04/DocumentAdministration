using System;
using System.Collections.Generic;
using System.Net;

namespace DocumentAdministration.API.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public ValidationException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
           
        }
    }
}

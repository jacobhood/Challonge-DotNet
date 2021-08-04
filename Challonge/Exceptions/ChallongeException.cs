using Newtonsoft.Json;
using System;
using System.Net;

namespace Challonge.Exceptions
{
    public class ChallongeException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ChallongeException() { }

        public ChallongeException(string message) : base(message) { }

        public ChallongeException(string message, Exception inner) : base(message, inner) { }

        public ChallongeException(string responseText, HttpStatusCode statusCode) : this(
            statusCode switch
            {
                HttpStatusCode.Unauthorized => "Unauthorized - Invalid credentials or " +
                    "insufficient permissions.",
                HttpStatusCode.NotAcceptable => "Invalid response format specified. " +
                    "This is most likely an internal Challonge-DotNet error, " +
                    "please report an issue in the Github repository.",
                HttpStatusCode.InternalServerError => "An unspecified Challonge server error occurred.",
                HttpStatusCode.UnprocessableEntity or HttpStatusCode.NotFound =>
                    JsonConvert.DeserializeObject<ErrorResponse>(responseText).Message,
                _ => responseText
            })
        {
            StatusCode = statusCode;
        }
    }
}
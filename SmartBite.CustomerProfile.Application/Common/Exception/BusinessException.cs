using System.Net;

namespace SmartBite.CustomerProfile.Application.Common.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public BusinessException()
        {
        }

        public BusinessException(string? message) : base(message)
        {
        }

        public BusinessException(string? message, HttpStatusCode statusCode) : base(message)
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
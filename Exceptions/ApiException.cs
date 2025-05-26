namespace MVCaptcha.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message,
                            int statusCode = 500,
                            Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    public class ValidationException : ApiException
    {
        public ValidationException()
            : base("Validation errors occurred", 400)
        {
        }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string message)
            : base(message, 404)
        {
        }
    }

    public class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base(message, 400)
        {
        }
    }
}

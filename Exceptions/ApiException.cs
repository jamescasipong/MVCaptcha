namespace MVCaptcha.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message,
                            int statusCode = 500,
                            Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    public class ValidationException : ApiException
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(Dictionary<string, string[]> errors)
            : base("Validation errors occurred", 400)
        {
            Errors = errors;
        }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string message)
            : base(message, 404)
        {
        }
    }
}

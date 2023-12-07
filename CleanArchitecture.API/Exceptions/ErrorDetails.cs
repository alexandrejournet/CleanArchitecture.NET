namespace CleanArchitecture.API.Exceptions
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}

namespace Shared.Exceptions
{
    public abstract class BadRequestException : Exception 
    {
        protected BadRequestException(string message, string[]? errors = null) : base(message) {
            Errors = errors ?? [];
        }

        public string[] Errors { get; }
    }
}

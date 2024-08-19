namespace Core.Results
{
    public class Result
    {

        public string? Message { get; set; }
        public ResultStatus Status { get; set; }
        public ICollection<string> Errors { get; }

        public Result(string? message, ResultStatus status = ResultStatus.Ok)
        {
            Message = message;
            Status = status;
        }

        public Result(string? message, ResultStatus status = ResultStatus.Ok, ICollection<string> errors = null)
        {
            Message = message;
            Status = status;
            Errors = errors;
        }

        public Result(ResultStatus status) : this("", status)
        {

        }

    }

    public enum ResultStatus
    {
        Ok,
        Error,
        Forbidden,
        Unauthorized,
        Invalid,
        NotFound
    }
}

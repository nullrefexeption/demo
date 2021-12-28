using Models.Enums;

namespace Models.Models
{
    public interface IResponse
    {
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }

    public class Response : IResponse
    {
        public Status Status { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<string> WarningMessages { get; set; }
        public List<string> SuccessMessages { get; set; }
        public Dictionary<string, List<string>> FieldErrorMessages { get; set; }

        public Response()
        {
            ErrorMessages = new List<string>();
            WarningMessages = new List<string>();
            FieldErrorMessages = new Dictionary<string, List<string>>();
            SuccessMessages = new List<string>();
        }

        public Response SetStatusSuccess()
        {
            Status = Status.Success;
            return this;
        }

        public Response SetStatusError()
        {
            Status = Status.Error;
            return this;
        }

        public Response AddSuccessMessage(string message)
        {
            SuccessMessages.Add(message);
            return this;
        }

        public Response AddWarningMessage(string message)
        {
            WarningMessages.Add(message);
            return this;
        }

        public Response AddErrorMessage(string message)
        {
            ErrorMessages.Add(message);
            return this;
        }
    }
}

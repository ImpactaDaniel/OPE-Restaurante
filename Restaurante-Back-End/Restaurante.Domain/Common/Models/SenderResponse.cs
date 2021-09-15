namespace Restaurante.Domain.Common.Models
{
    public class SenderResponse
    {
        public bool Success { get; }
        public string Error { get;  }
        public SenderResponse(bool success, string error)
        {
            Success = success;
            Error = error;
        }
    }
}

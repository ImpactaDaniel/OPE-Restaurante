namespace Restaurante.Domain.Common.Models
{
    public class Notification
    {
        public virtual int Code { get; private set; }
        public virtual string Message { get; private set; }
        public Notification(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}

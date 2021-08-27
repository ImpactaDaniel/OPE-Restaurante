namespace Restaurante.Domain.Common.Models
{
    public class Message
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }
        public Message(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}

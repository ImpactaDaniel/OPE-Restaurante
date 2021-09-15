using Restaurante.Domain.Common.Models;

namespace Restaurante.Application.Common.Models
{
    public class EmailMessage : Message
    {
        public EmailMessage(string to, string subject, string body) : base(to, subject, body)
        {
        }
    }
}

using Restaurante.Domain.BasicEntities.Common.Interfaces;

namespace Restaurante.Application.BasicEntities.Requests.Banks.Models
{
    public class CreateBankRequest : IBasicEntity
    {
        public string Name { get; set; }
    }
}

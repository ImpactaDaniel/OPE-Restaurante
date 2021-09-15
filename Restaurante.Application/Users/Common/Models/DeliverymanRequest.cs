using Restaurante.Application.Common;

namespace Restaurante.Application.Users.Common.Models
{
    public class DeliverymanRequest<TRequest> : EmployeeRequest<TRequest>
        where TRequest : EntityRequest<int>
    {
        public VehicleRequest Vehicle { get; set; }
    }

    public class VehicleRequest
    {
        public string MotorcycleBrand { get; set; }
        public int MotorcycleYear { get; set; }
        public string MotorcycleModel { get; set; }
    }
}

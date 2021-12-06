namespace Restaurante.Application.Baskets.Common.Models
{
    public class BasketRequest
    {
        public int CustomerId { get; set; }
    }

    public class BasketItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Obs { get; set; }
    }
}

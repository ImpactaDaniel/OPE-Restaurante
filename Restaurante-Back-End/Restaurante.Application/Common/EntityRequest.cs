namespace Restaurante.Application.Common
{
    public class EntityRequest<TId>
    {
        public TId Id { get; set; } = default;
    }
}

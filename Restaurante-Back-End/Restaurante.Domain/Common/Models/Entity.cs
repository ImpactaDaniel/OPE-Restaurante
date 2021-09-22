using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Users.Exceptions;

namespace Restaurante.Domain.Common.Models
{
    public abstract class Entity<TId> : IEntity
    {
        public TId Id { get; private set; } = default;
        protected void ValidateNullString(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
                throw new UserException($"{name} precisa ter um valor!", NotificationKeys.InvalidEntity);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Id.Equals(default) || other.Id.Equals(default))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> first, Entity<TId> second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return first.Equals(second);
        }

        public static bool operator !=(Entity<TId> first, Entity<TId> second) => !(first == second);

        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
    }
}

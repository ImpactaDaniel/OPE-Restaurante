using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Models;

namespace Restaurante.Application.Common.Helper
{
    public class NotificationHelper
    {
        public static Notification EntityNotFound(string entityName) =>
            new Notification((int)NotificationKeys.EntityNotFound, $"{entityName} não encontrado!");

        public static Notification InvalidEmail() =>
            new Notification((int)NotificationKeys.InvalidEmail, "E-mail inválido");

        public static Notification InvalidPassword() =>
            new Notification((int)NotificationKeys.InvalidPassword, "Senha inválida!");

        public static Notification FromException(RestauranteException e) =>
            new Notification((int)e.Code, e.Message);

        public static Notification DoesntHavePermission(string nameUser, string toDo) =>
            new Notification((int)NotificationKeys.DoesntHavePermission, $"{nameUser} não tem permissão para {toDo}");

        public static Notification InvalidCredentials() =>
            new Notification((int)NotificationKeys.DoesntHavePermission, $"Credenciais inválidas!");

        public static Notification InvalidEmailOrPassword() =>
            new Notification((int)NotificationKeys.InvalidEmailOrPassword, "E-mail ou senha inválidos!");
    }
}

using Restaurante.Domain.Common.Models;
using Restaurante.Infra.Common.Enums;
using System;

namespace Restaurante.Infra.Common.Helper
{
    public class NotificationHelper
    {
        public static Notification EntityNotFound(string entityName) =>
            new Notification((int)NotificationKeys.EntityNotFound, $"{entityName} não encontrado!");

        public static Notification InvalidEmail() =>
            new Notification((int)NotificationKeys.InvalidEmail, "E-mail inválido");

        public static Notification InvalidPassword() =>
            new Notification((int)NotificationKeys.InvalidPassword, "Senha inválida!");

        public static Notification FromException(Exception e) =>
            new Notification((int)NotificationKeys.Error, e.Message);

        public static Notification DoesntHavePermission(string nameUser, string toDo) =>
            new Notification((int)NotificationKeys.DoesntHavePermission, $"{nameUser} não tem permissão para {toDo}");
    }
}

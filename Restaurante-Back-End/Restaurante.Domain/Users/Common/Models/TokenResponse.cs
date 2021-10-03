using System;

namespace Restaurante.Domain.Users.Common.Models
{
    public class TokenResponse
    {
        public string Token { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public DateTime ValidTo { get; private set; }
        public bool IsChangePasswordRequired { get; private set; }
        public TokenResponse(string token, DateTime validFrom, DateTime validTo, bool isChangePasswordRequired)
        {
            Token = token;
            ValidFrom = validFrom;
            ValidTo = validTo;
            IsChangePasswordRequired = isChangePasswordRequired;
        }
    }
}

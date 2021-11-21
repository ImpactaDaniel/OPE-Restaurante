using System;

#nullable enable

namespace Restaurante.Application.Common
{
    public class Response<T>
    {
        public bool Success { get; }
        public T? Result { get; }
        public Response(bool success, T result)
        {
            Success = success;
            Result = result;
        }

    }
}

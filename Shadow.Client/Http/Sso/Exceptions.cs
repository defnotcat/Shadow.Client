using System;

namespace Shadow.Client.Http.Sso
{
    public class SsoAuthenticationFailedException : Exception
    {
        public SsoAuthenticationFailedException(string? message) : base(message)
        {
        }
    }
}
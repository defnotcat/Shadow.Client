using System;

namespace Shadow.Client.Http.Gap
{
    public class TwoFactorRequiredException : Exception
    {
        public TwoFactorRequiredException(string? message) : base(message)
        {
        }
    }

    public class VmNotStartedYetException : Exception
    {
        public VmNotStartedYetException(string? message) : base(message)
        {
        }
    }
    
}
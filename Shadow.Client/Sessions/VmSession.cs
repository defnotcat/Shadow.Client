using System;
using Shadow.Client.Models;

namespace Shadow.Client.Sessions
{
    public class VmSession : IDisposable
    {
        
        public ShadowVM ParentVm { get; set; }
        
        public VmSession(ShadowVM parentVm)
        {
            ParentVm = parentVm;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using Shadow.Client.Models;
using Shadow.Client.Networking.Channels.VmProxy;

namespace Shadow.Client.Sessions
{
    public class VmSession : IDisposable
    {
        
        public ShadowVM ParentVm { get; set; }
        
        public IVmProxyChannel VmProxyChannel { get; set; }
        
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
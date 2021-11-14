using Shadow.Client.Http.Gap;

namespace Shadow.Client.Models
{
    public class ShadowVM
    {
        
        public GapSession Session { get; set; }
        
        public Identity Identity { get; set; }
        
        public Credentials Credentials { get; set; }
        
        public GapApiProvider GapProvider { get; set; }
        
        public VmLocation Location { get; set; }

        public ShadowVM(GapSession session, Identity identity, Credentials credentials)
        {
            Session = session;
            Identity = identity;
            Credentials = credentials;
            GapProvider = new GapApiProvider(identity);
            GapProvider.SetRecommendedHost(credentials.Email);
        }

    }
}
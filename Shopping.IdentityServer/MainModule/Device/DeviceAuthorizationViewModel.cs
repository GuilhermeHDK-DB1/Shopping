using IdentityServerHost.Quickstart.UI;
using Shopping.IdentityServer.MainModule.Consent;

namespace Shopping.IdentityServer.MainModule.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}
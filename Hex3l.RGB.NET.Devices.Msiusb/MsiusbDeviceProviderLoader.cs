using RGB.NET.Core;

namespace Hex3l.RGB.NET.Devices.Msiusb
{
    public class MsiusbDeviceProviderLoader : IRGBDeviceProviderLoader
    {
        #region Properties & Fields

        public bool RequiresInitialization => false;

        #endregion

        #region Methods

        public IRGBDeviceProvider GetDeviceProvider() => MsiusbDeviceProvider.Instance;

        #endregion
    }
}

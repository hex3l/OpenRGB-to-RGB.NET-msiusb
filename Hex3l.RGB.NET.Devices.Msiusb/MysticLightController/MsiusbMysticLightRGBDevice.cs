using RGB.NET.Core;
using Hex3l.RGB.NET.Devices.Msiusb.Generic;

namespace Hex3l.RGB.NET.Devices.Msiusb.MysticLightController
{
    public class MsiusbMysticLightRGBDevice : MsiusbRGBDevice<MsiusbRGBDeviceInfo>
    {
        #region Constructors

        internal MsiusbMysticLightRGBDevice(MsiusbRGBDeviceInfo info)
            : base(info)
        { }

        #endregion

        #region Methods
        protected override void InitializeLayout(int ledCount)
        {
            for (int i = 0; i < ledCount; i++)
            {
                InitializeLed(LedId.Mainboard1 + i, new Rectangle(i * 40, 0, 40, 8));
            }

            //TODO DarthAffe 07.10.2017: We don't know the model, how to save layouts and images?
            ApplyLayoutFromFile(PathHelper.GetAbsolutePath($@"Layouts\MSIusb\Controller\{DeviceInfo.Model.Replace(" ", string.Empty).ToUpper()}.xml"), null);
        }

        protected override object CreateLedCustomData(LedId ledId) => (int)ledId - (int)LedId.Mainboard1;

        public override void SyncBack()
        { }

        #endregion
    }
}

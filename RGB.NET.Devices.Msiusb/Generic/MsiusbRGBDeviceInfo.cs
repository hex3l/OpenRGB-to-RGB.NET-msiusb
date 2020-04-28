using RGB.NET.Core;
using System;

namespace RGB.NET.Devices.Msiusb.Generic
{
    public class MsiusbRGBDeviceInfo : IRGBDeviceInfo 
    {
        #region Properties & Fields
        public RGBDeviceType DeviceType { get; }

        public int MsiDeviceID { get; }

        public string DeviceName { get; }

        public string Manufacturer { get; }

        public string Model { get; }

        public Uri Image { get; set; }

        public bool SupportsSyncBack => false;

        public RGBDeviceLighting Lighting => RGBDeviceLighting.Key;

        #endregion

        #region Constructors

        internal MsiusbRGBDeviceInfo(RGBDeviceType deviceType, int deviceID, string manufacturer = "MSI", string model = "Generic MysticLight USB Controller")
        {
            this.DeviceType = deviceType;
            this.MsiDeviceID = deviceID;
            this.Manufacturer = manufacturer;
            this.Model = model;

            DeviceName = $"{Manufacturer} {Model}";
        }

        #endregion
    }
}

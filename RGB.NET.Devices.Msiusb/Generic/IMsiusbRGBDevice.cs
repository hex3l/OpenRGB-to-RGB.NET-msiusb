using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RGB.NET.Devices.Msiusb.Generic
{
    interface IMsiusbRGBDevice : IRGBDevice
    {
        void Initialize(MsiusbDeviceUpdateQueue updateQueue, int ledCount);
    }
}

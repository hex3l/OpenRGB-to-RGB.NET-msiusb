using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hex3l.RGB.NET.Devices.Msiusb.Generic
{
    interface IMsiusbRGBDevice : IRGBDevice
    {
        void Initialize(MsiusbDeviceUpdateQueue updateQueue, int ledCount);
    }
}

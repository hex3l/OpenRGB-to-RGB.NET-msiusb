using RGB.NET.Core;
using Hex3l.RGB.NET.Devices.Msiusb.Native;
using System.Collections.Generic;

namespace Hex3l.RGB.NET.Devices.Msiusb.Generic
{
    public class MsiusbDeviceUpdateQueue : UpdateQueue
    {
        #region Properties & Fields

        private int _deviceID;

        #endregion

        #region Constructors

        public MsiusbDeviceUpdateQueue(IDeviceUpdateTrigger updateTrigger, int deviceID)
            : base(updateTrigger)
        {
            this._deviceID = deviceID;
        }

        #endregion

        #region Methods

        protected override void Update(Dictionary<object, Color> dataSet)
        {
            foreach (KeyValuePair<object, Color> data in dataSet)
                _OpenRGB_MSI_USB.SetZoneColor(_deviceID, (int)data.Key, data.Value.GetR(), data.Value.GetG(), data.Value.GetB());
        }

        #endregion
    }
}

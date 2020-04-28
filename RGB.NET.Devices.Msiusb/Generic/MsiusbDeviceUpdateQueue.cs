using RGB.NET.Core;
using RGB.NET.Devices.Msiusb.Native;
using System.Collections.Generic;

namespace RGB.NET.Devices.Msiusb.Generic
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
                _OpenRGB_MSI_USB.SetColor(_deviceID, data.Value.GetR(), data.Value.GetG(), data.Value.GetB());
        }

        #endregion
    }
}

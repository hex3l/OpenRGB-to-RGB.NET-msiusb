using RGB.NET.Core;
using System.Collections.Generic;
using System.Linq;

namespace RGB.NET.Devices.Msiusb.Generic
{
    public abstract class MsiusbRGBDevice<TDeviceInfo> : AbstractRGBDevice<TDeviceInfo>, IMsiusbRGBDevice
         where TDeviceInfo : MsiusbRGBDeviceInfo
    {
        #region Properties & Fields

        public override TDeviceInfo DeviceInfo { get; }

        protected MsiusbDeviceUpdateQueue DeviceUpdateQueue { get; set; }

        #endregion

        #region Constructors

        protected MsiusbRGBDevice(TDeviceInfo info)
        {
            this.DeviceInfo = info;
        }

        #endregion

        #region Methods

        public void Initialize(MsiusbDeviceUpdateQueue updateQueue, int ledCount)
        {
            DeviceUpdateQueue = updateQueue;

            InitializeLayout(ledCount);

            if (Size == Size.Invalid)
            {
                Rectangle ledRectangle = new Rectangle(this.Select(x => x.LedRectangle));
                Size = ledRectangle.Size + new Size(ledRectangle.Location.X, ledRectangle.Location.Y);
            }
        }

        protected abstract void InitializeLayout(int ledCount);

        protected override void UpdateLeds(IEnumerable<Led> ledsToUpdate)
            => DeviceUpdateQueue.SetData(ledsToUpdate.Where(x => (x.Color.A > 0) && (x.CustomData is int)));

        #endregion
    }
}

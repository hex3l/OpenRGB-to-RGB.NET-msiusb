using RGB.NET.Core;
using Hex3l.RGB.NET.Devices.Msiusb.Generic;
using Hex3l.RGB.NET.Devices.Msiusb.MysticLightController;
using Hex3l.RGB.NET.Devices.Msiusb.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Hex3l.RGB.NET.Devices.Msiusb
{
    public class MsiusbDeviceProvider : IRGBDeviceProvider
    {
        #region Properties & Fields

        private static MsiusbDeviceProvider _instance;
        public static MsiusbDeviceProvider Instance => _instance ?? new MsiusbDeviceProvider();

        public static List<string> PossibleX86NativePaths { get; } = new List<string> { "x86/OpenRGB_MSI_USB.dll" };

        public static List<string> PossibleX64NativePaths { get; } = new List<string> { "x64/OpenRGB_MSI_USB.dll" };

        public bool IsInitialized { get; private set; }

        public string LoadedArchitecture => _OpenRGB_MSI_USB.LoadedArchitecture;

        public bool HasExclusiveAccess { get; private set; }

        public IEnumerable<IRGBDevice> Devices { get; private set; }

        public Func<CultureInfo> GetCulture { get; set; } = CultureHelper.GetCurrentCulture;

        public DeviceUpdateTrigger UpdateTrigger { get; }

        #endregion

        #region Constructors

        public MsiusbDeviceProvider()
        {
            if (_instance != null) throw new InvalidOperationException($"There can be only one instance of type {nameof(MsiusbDeviceProvider)}");
            _instance = this;

            UpdateTrigger = new DeviceUpdateTrigger();
        }

        #endregion

        #region Methods

        public bool Initialize(RGBDeviceType loadFilter = RGBDeviceType.All, bool exclusiveAccessIfPossible = false, bool throwExceptions = false)
        {
            IsInitialized = false;

            try
            {
                UpdateTrigger?.Stop();

                _OpenRGB_MSI_USB.Reload();

                IList<IRGBDevice> devices = new List<IRGBDevice>();

                _OpenRGB_MSI_USB.Initialize();

                int controllers = _OpenRGB_MSI_USB.GetNumberOfControllers();

                if(controllers > 0)
                {

                    for (int i = 0; i < controllers; i++)
                    {
                        string name = _OpenRGB_MSI_USB.GetControllerName(i);
                        _OpenRGB_MSI_USB.GetControllerZones(i, out string[] zoneNames, out uint[] zoneLeds);
                        MsiusbDeviceUpdateQueue updateQueue = new MsiusbDeviceUpdateQueue(UpdateTrigger, i);
                        IMsiusbRGBDevice motherboard = new MsiusbMysticLightRGBDevice(new MsiusbRGBDeviceInfo(RGBDeviceType.Mainboard, i, "MSI-USB", name));
                        motherboard.Initialize(updateQueue, zoneNames.Length);
                        devices.Add(motherboard);
                    }
                }

                UpdateTrigger?.Start();

                Devices = new ReadOnlyCollection<IRGBDevice>(devices);
                IsInitialized = true;
            }
            catch
            {
                if (throwExceptions)
                    throw;
                return false;
            }

            return true;
        }

        public void ResetDevices()
        {
            //TODO: Implement
        }

        public void Dispose()
        { }

        #endregion
    }
}

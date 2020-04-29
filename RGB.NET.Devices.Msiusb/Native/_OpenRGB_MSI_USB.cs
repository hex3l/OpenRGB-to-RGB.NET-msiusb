using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RGB.NET.Devices.Msiusb.Native
{
    internal static class _OpenRGB_MSI_USB
    {
        internal static string LoadedArchitecture { get; private set; }

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern int init();

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern int getNumberOfControllers();

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        [return: MarshalAs(UnmanagedType.BStr)]
        private static extern string getControllerName(int controller_idx);

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        [return: MarshalAs(UnmanagedType.BStr)]
        private static extern string getControllerDescription(int controller_idx);

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern void getControllerZones(int controller_idx, [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] zonesArray, [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UINT)] out uint[] zonesLedsArray);

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern int setMode(int controller_idx, [MarshalAs(UnmanagedType.BStr)] string mode);

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern int setLedColor(int controller_idx, int led_idx, int r, int g, int b);

        [DllImport(@"OpenRGB_MSI_USB.dll")]
        private static extern int setZoneColor(int controller_idx, int zone_idx, int r, int g, int b);

        internal static void Initialize() => init();
        internal static int GetNumberOfControllers() => getNumberOfControllers();
        internal static string GetControllerName(int controller_idx) => getControllerName(controller_idx);
        internal static string GetControllerDescription(int controller_idx) => getControllerDescription(controller_idx);
        internal static void GetControllerZones(int i, out string[] zonesArray, out uint[] zonesLedsArray) => getControllerZones(i, out zonesArray, out zonesLedsArray);
        internal static void SetMode(int controller, string mode) => setMode(controller, mode);
        internal static void SetLedColor(int controller, int led, int r, int g, int b) => setLedColor(controller, led, r, g, b);
        internal static void SetZoneColor(int controller, int zone, int r, int g, int b) => setZoneColor(controller, zone, r, g, b);

        /*
        #region Libary Management

        private static IntPtr _dllHandle = IntPtr.Zero;

        /// <summary>
        /// Gets the loaded architecture (x64/x86).
        /// </summary>
        internal static string LoadedArchitecture { get; private set; }

        /// <summary>
        /// Reloads the SDK.
        /// </summary>
        internal static void Reload()
        {
            UnloadMsiusb();
            LoadMsiusb();
        }

        private static void LoadMsiusb()
        {
            if (_dllHandle != IntPtr.Zero) return;

            // HACK: Load library at runtime to support both, x86 and x64 with one managed dll
            List<string> possiblePathList = Environment.Is64BitProcess ? MsiusbDeviceProvider.PossibleX64NativePaths : MsiusbDeviceProvider.PossibleX86NativePaths;
            string dllPath = possiblePathList.FirstOrDefault(File.Exists);
            if (dllPath == null) throw new  RGBDeviceException($"Can't find the Msi-SDK at one of the expected locations:\r\n '{string.Join("\r\n", possiblePathList.Select(Path.GetFullPath))}'");

            SetDllDirectory(Path.GetDirectoryName(Path.GetFullPath(dllPath)));

            _dllHandle = LoadLibrary(dllPath);

            _initPointer = (InitializePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "init"), typeof(InitializePointer));
            _getNumberOfControllersPointer = (GetNumberOfControllersPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "getNumberOfControllers"), typeof(GetNumberOfControllersPointer));
            _setColorPointer = (SetColorPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "setColor"), typeof(SetColorPointer));
            _setModePointer = (SetModePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "setMode"), typeof(SetModePointer));
        }

        private static void UnloadMsiusb()
        {
            if (_dllHandle == IntPtr.Zero) return;

            // ReSharper disable once EmptyEmbeddedStatement - DarthAffe 07.10.2017: We might need to reduce the internal reference counter more than once to set the library free
            while (FreeLibrary(_dllHandle)) ;
            _dllHandle = IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr dllHandle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr dllHandle, string name);

        #endregion

        #region SDK-METHODS

        #region Pointers

        private static InitializePointer _initPointer;
        private static GetNumberOfControllersPointer _getNumberOfControllersPointer;
        private static SetColorPointer _setColorPointer;
        private static SetModePointer _setModePointer;

        #endregion

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void InitializePointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GetNumberOfControllersPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetColorPointer(
            [MarshalAs(UnmanagedType.I4)] int controller,
            [MarshalAs(UnmanagedType.I4)] int r,
            [MarshalAs(UnmanagedType.I4)] int g,
            [MarshalAs(UnmanagedType.I4)] int b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetModePointer(
            [MarshalAs(UnmanagedType.I4)] int controller,
            [MarshalAs(UnmanagedType.BStr)] string mode);
        #endregion

        internal static void Initialize() => _initPointer();
        internal static int GetNumberOfControllers() => _getNumberOfControllersPointer();
        internal static void SetColor(int controller, int r, int g, int b) => _setColorPointer(controller, r, g, b);
        internal static void SetMode(int controller, string mode) => _setModePointer(controller, mode);

        #endregion
        */
    }
}

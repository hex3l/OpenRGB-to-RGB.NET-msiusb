# OpenRGB to RGB.NET msiusb
Compatibility layer for RGB.NET

This device provider uses OpenRGB's MysticLightUSBControllers implementation to control MysticLight USB controllers (only motherboards x570?)

**_Keep in mind that this project uses [OpenRGB](https://gitlab.com/CalcProgrammer1/OpenRGB) directly. As OpenRGB, I will not be liable for any damage._**

## Solution

The solution has two projects

#### _Hex3l.RGB.NET.Devices.Msiusb_
RGB.NET DeviceProvider, it will take care of loading OpenRGB_MSI_USB.dll and communicate with it.

#### _OpenRGB_MSI_USB_
Minimal implementation of OpenRGB features required by this layer. It includes directly (or using git patches) the code/implementations made by _OpenRGB_


## How to build and use

1. Clone the repo with its submodules.
2. Apply git patch in `OpenRGB_MSI_USB/ORGB-patches` to `OpenRGB_MSI_USB/OpenRGB`
3. Apply git patch in `OpenRGB_MSI_USB/ORGB-min-api-dll-patches` to `OpenRGB_MSI_USB/OpenRGB-min-api-dll-base`
4. Build `Hex3l.RGB.NET.Devices.Msiusb`(Release) (it will also build _OpenRGB_MSI_USB_)
5. Get the 2 dlls and place
   - `Hex3l.RGB.NET.Devices.Msiusb.dll` in your `DeviceProvider` folder (the folder you are using to store your `DeviceProviders`)
   - `OpenRGB_MSI_USB.dll` in 
      - x86: `<app root>/x86`
      - x64: `<app root>/x64`
6. You will also need libusb-1.0.dll in your root app directory, you can get it from 
   - x86:`OpenRGB_MSI_USB/OpenRGB/dependencies/libusb-1.0.22/MS32/dll`
   - x64:`OpenRGB_MSI_USB/OpenRGB/dependencies/libusb-1.0.22/MS64/dll`


## Tested Device

 - MSI MEG X570 UNIFY

#pragma once

#include <Windows.h>
#include <vector>

#define MSI_USB_API __declspec(dllexport)

extern "C" MSI_USB_API void init();

extern "C" MSI_USB_API void getControllerZones(int i, SAFEARRAY** zonesArray, SAFEARRAY** zonesLedsArray);

extern "C" MSI_USB_API BSTR getControllerName(int i);

extern "C" MSI_USB_API int getNumberOfControllers();

extern "C" MSI_USB_API void setColor(int i, int r, int g, int b);

extern "C" MSI_USB_API void setMode(int i, unsigned char mode);
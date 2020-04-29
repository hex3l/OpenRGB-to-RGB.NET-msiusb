#include "openrgb_msi_usb.h"
#include <vector>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <comutil.h>

#include "RGBController.h"
#include "MSIMysticLightControllerDetect.h"

std::vector<RGBController*> rgb_controllers;

/*unsigned char* GetDeviceDescription(int i, unsigned char* buff) {
    RGBController* controller = rgb_controllers.at(i);
    return controller->GetDeviceDescription();
}*/

void setColor(int i, int r, int g, int b) {
    RGBController* controller = rgb_controllers.at(i);
    RGBColor color = ToRGBColor(r >> 2, g >> 2, b >> 2);
    return controller->SetAllLEDs(color);
}

BSTR getControllerName(int i) {
    RGBController* controller = rgb_controllers.at(i);
    _bstr_t conv = controller->name.c_str();
    
    return SysAllocString(conv);
}

void getControllerZones(int i, SAFEARRAY** zonesArray, SAFEARRAY** zonesLedsArray) {
    RGBController* controller = rgb_controllers.at(i);

    if (controller->zones.size() > 0)
    {
        SAFEARRAYBOUND  Bound;
        Bound.lLbound = 0;
        Bound.cElements = controller->zones.size();

        *zonesArray = SafeArrayCreate(VT_BSTR, 1, &Bound);

        BSTR* pData;
        HRESULT hr = SafeArrayAccessData(*zonesArray, (void**)&pData);
        if (SUCCEEDED(hr))
        {
            for (DWORD i = 0; i < controller->zones.size(); i++)
            {
                _bstr_t conv = controller->zones.at(i).name.c_str();
                *pData++ = SysAllocString(conv);
            }
            SafeArrayUnaccessData(*zonesArray);
        }

        *zonesLedsArray = SafeArrayCreate(VT_UINT, 1, &Bound);

        unsigned int HUGEP* pIntData;
        HRESULT hr2 = SafeArrayAccessData(*zonesLedsArray, (void HUGEP* FAR*)&pIntData);
        if (SUCCEEDED(hr2))
        {
            for (DWORD i = 0; i < controller->zones.size(); i++)
            {
                *pIntData++ = controller->zones.at(i).leds_count;
            }
            SafeArrayUnaccessData(*zonesLedsArray);
        }
    }
    else
    {
        zonesArray = nullptr;
        zonesLedsArray = nullptr;
    }
}

void setZoneColor(int i, std::string zone, int r, int g, int b) {

}

void setMode(int i, unsigned char mode) {
    RGBController* controller = rgb_controllers.at(i);
    return controller->SetMode(mode);
}

void init() {
    DetectMSIMysticLightControllers(rgb_controllers);
}

int getNumberOfControllers() {
    return rgb_controllers.size();
}
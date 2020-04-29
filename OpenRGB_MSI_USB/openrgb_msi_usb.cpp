#include "openrgb_msi_usb.h"
#include <vector>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <comutil.h>

#include "RGBController.h"
#include "MSIMysticLightControllerDetect.h"

// List of detected controllers
std::vector<RGBController*> rgb_controllers;

// Directly using ToRGBColor was overflowing(?) the 2 most significant bits.
// Pre-shifting seems a good temporary solution
RGBColor toRgbColor(int r, int g, int b) {
    return ToRGBColor(r >> 2, g >> 2, b >> 2);
}

// Initialize the lib and detects controllers
void init() {
    DetectMSIMysticLightControllers(rgb_controllers);
}

/**
* Return number of detected controllers
*
* @return length of 'rgb_controllers'
*/
int getNumberOfControllers() {
    return rgb_controllers.size();
}

/**
 * Returns controller's name
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @return controller's name
 */
BSTR getControllerName(int controller_idx) {
    RGBController* controller = rgb_controllers.at(controller_idx);
    _bstr_t conv = controller->name.c_str();

    return SysAllocString(conv);
}

/**
 * Returns controller's description
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @return controller's description
 */
BSTR getControllerDescription(int controller_idx) {
    RGBController* controller = rgb_controllers.at(controller_idx);
    _bstr_t conv = controller->description.c_str();
    return conv;
}

/**
 * Returns controller zone's name and its leds number
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @param[out] zonesArray array of zone's name
 * @param[out] zonesLedsArray array of zone's leds
 */
void getControllerZones(int controller_idx, SAFEARRAY** zonesArray, SAFEARRAY** zonesLedsArray) {
    RGBController* controller = rgb_controllers.at(controller_idx);

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
        HRESULT hr2 = SafeArrayAccessData(*zonesLedsArray, (void HUGEP * FAR*) & pIntData);
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

/**
 * Sets the controller to specified mode
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @param[in] mode mode id
 */
void setMode(int controller_idx, unsigned char mode) {
    RGBController* controller = rgb_controllers.at(controller_idx);
    return controller->SetMode(mode);
}

/**
 * Sets the specified led color
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @param[in] led_idx index of the led we need to change
 * @param[in] r red
 * @param[in] g green
 * @param[in] b blue
 */
void setLedColor(int controller_idx, int led_idx, int r, int g, int b) {
    RGBController* controller = rgb_controllers.at(controller_idx);
    RGBColor color = toRgbColor(r, g, b);
    return controller->SetLED(led_idx, color);
}

/**
 * Sets the specified zone's leds color
 *
 * @param[in] controller_idx index of controller in rgb_controllers
 * @param[in] led_idx index of the led we need to change
 * @param[in] r red
 * @param[in] g green
 * @param[in] b blue
 */
void setZoneColor(int controller_idx, int zone_idx, int r, int g, int b) {
    RGBController* controller = rgb_controllers.at(controller_idx);
    RGBColor color = toRgbColor(r, g, b);
    return controller->SetAllZoneLEDs(zone_idx, color);
}

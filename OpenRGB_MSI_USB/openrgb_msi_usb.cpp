#include "openrgb_msi_usb.h"
#include <vector>
#include <stdio.h>
#include <stdlib.h>

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
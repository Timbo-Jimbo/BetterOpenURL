// BetterOpenURLWrapper.m

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "BetterOpenURL-Swift.h"

bool BetterOpenURL_supportsSafariView(void) {
    return [BetterOpenURL supportsSafariView];
}

void BetterOpenURL_openSafariViewViaUnity(
    const char* url,
    bool customColor,
    const char* barTintColorHex,
    int modalTransitionStyle,
    int modalPresentationStyle,
    bool barCollapsingEnabled,
    int dismissButtonStyle
) {
    [BetterOpenURL openSafariViewViaUnityWithUrl:url
        customColor:customColor
        barTintColorHex:barTintColorHex
        modalTransitionStyle:modalTransitionStyle
        modelPresentationStyle:modalPresentationStyle
        barCollapsingEnabled:barCollapsingEnabled
        dismissButtonStyle:dismissButtonStyle
    ];
}

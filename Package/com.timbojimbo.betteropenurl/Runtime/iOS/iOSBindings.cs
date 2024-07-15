using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

internal class iOSBindings
{
    [DllImport("__Internal")]
    private static extern bool BetterOpenURL_supportsSafariView();

    [DllImport("__Internal")]
    private static extern void BetterOpenURL_openSafariViewViaUnity(
        string url,
        bool customColor,
        string barTintColorHex,
        iOSTransitionStyle modalTransitionStyle,
        iOSPresentationStyle modalPresentationStyle,
        bool barCollapsingEnabled,
        iOSDismissButtonStyle dismissButtonStyle
    );

    public static bool SupportsSafariView()
    {
        return BetterOpenURL_supportsSafariView();
    }

    public static void OpenSafariView(
        string url,
        bool customColor,
        string barTintColorHex,
        iOSTransitionStyle modalTransitionStyle,
        iOSPresentationStyle modalPresentationStyle,
        bool barCollapsingEnabled,
        iOSDismissButtonStyle dismissButtonStyle
    )
    {
        BetterOpenURL_openSafariViewViaUnity(url, customColor, barTintColorHex, modalTransitionStyle, modalPresentationStyle, barCollapsingEnabled, dismissButtonStyle);
    }
}

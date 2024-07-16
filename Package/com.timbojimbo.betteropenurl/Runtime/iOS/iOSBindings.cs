#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.iOS
{
    internal class iOSBindings
    {
        [DllImport("__Internal")]
        private static extern bool BetterOpenURL_supportsSafariView();

        [DllImport("__Internal")]
        private static extern void BetterOpenURL_openSafariView(
            string url,
            bool customColor,
            string barTintColorHex,
            int modalTransitionStyle,
            int modalPresentationStyle,
            bool barCollapsingEnabled,
            int dismissButtonStyle
        );

        public static bool SupportsSafariView()
        {
            return BetterOpenURL_supportsSafariView();
        }

        public static void OpenSafariView(
            string url,
            bool customColor,
            Color barTintColor,
            iOSTransitionStyle modalTransitionStyle,
            iOSPresentationStyle modalPresentationStyle,
            bool barCollapsingEnabled,
            iOSDismissButtonStyle dismissButtonStyle
        )
        {
            BetterOpenURL_openSafariView(
                url, 
                customColor, 
                $"#{ColorUtility.ToHtmlStringRGB(barTintColor)}", 
                (int)modalTransitionStyle, 
                (int)modalPresentationStyle, 
                barCollapsingEnabled, 
                (int)dismissButtonStyle
            );
        }
    }
}
#endif
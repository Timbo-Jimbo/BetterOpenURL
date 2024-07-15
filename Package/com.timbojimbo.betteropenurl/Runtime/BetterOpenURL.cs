using System;
using TimboJimbo.BetterOpenURL.Android;
using TimboJimbo.BetterOpenURL.iOS;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL
{
    [Serializable]
    public class BetterOpenURL
    {
        public AndroidSettings AndroidSettings = new ();
        public iOSSettings iOSSettings = new ();

        public void Open(string url)
        {
            #if UNITY_ANDROID
            if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
            {
                using (var UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var context = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaClass BetterOpenURLClass = new AndroidJavaClass("com.timbojimbo.betteropenurl.BetterOpenURL"))
                    {
                        BetterOpenURLClass.CallStatic("testLog");

                        var hasCustomTabsSupport = BetterOpenURLClass.CallStatic<bool>("hasCustomTabsSupport", context);
                        
                        if(hasCustomTabsSupport)
                        {
                            BetterOpenURLClass.CallStatic("openInCustomTab", context, url, AndroidSettings.CustomToolbarColors, ColorUtility.ToHtmlStringRGB(AndroidSettings.ToolbarColor), ColorUtility.ToHtmlStringRGB(AndroidSettings.ToolbarSecondaryColor), AndroidSettings.ShowTitle, AndroidSettings.UrlBarHidingEnabled, AndroidSettings.CustomAnimations, (int)AndroidSettings.ApplicationAnimation, (int)AndroidSettings.CustomTabAnimation);                                
                        }
                        else
                        {
                            Application.OpenURL(url);
                        }
                    }
                }
            }
            else
            #endif
            #if UNITY_IOS
            if (!Application.isEditor && Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if(iOSBindings.SupportsSafariView())
                {
                    iOSBindings.OpenSafariView(url, iOSSettings.CustomColor, ColorUtility.ToHtmlStringRGB(iOSSettings.BarTintColor), iOSSettings.TransitionStyle, iOSSettings.PresentationStyle, iOSSettings.BarCollapsingEnabled, iOSSettings.DismissButtonStyle);
                }
                else
                {
                    Application.OpenURL(url);
                }
            }
            else
            #endif
            {
                Application.OpenURL(url);
            }
        }

        public static void OpenWithDefaultSettings(string url)
        {
            var instance = new BetterOpenURL();
            instance.Open(url);
        }
    }
}

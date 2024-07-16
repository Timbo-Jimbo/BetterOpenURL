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
        public bool Logging = true;

        public void Open(string url)
        {
            Log($"Opening URL: {url}");
            
            #if UNITY_ANDROID
            if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
            {
                Log("Platform is Android");

                if(AndroidBindings.HasCustomTabsSupport())
                {
                    Log("Custom Tabs supported, opening URL with custom tabs");
                    AndroidBindings.OpenInCustomTab(url, AndroidSettings.CustomToolbarColors, AndroidSettings.ToolbarColor, AndroidSettings.ToolbarSecondaryColor, AndroidSettings.ShowTitle, AndroidSettings.UrlBarHidingEnabled, AndroidSettings.CustomAnimations, AndroidSettings.ApplicationAnimation, AndroidSettings.CustomTabAnimation);
                }
                else
                {
                    Log("Custom Tabs not supported, falling back to default behavior"); 
                    Application.OpenURL(url);
                }
            }
            else
            #endif
            #if UNITY_IOS
            if (!Application.isEditor && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer))
            {
                Log("Platform is iOS");
                
                if(iOSBindings.SupportsSafariView())
                {
                    Log("Safari View supported, opening URL with Safari View");
                    iOSBindings.OpenSafariView(url, iOSSettings.CustomColor, iOSSettings.BarTintColor, iOSSettings.TransitionStyle, iOSSettings.PresentationStyle, iOSSettings.BarCollapsingEnabled, iOSSettings.DismissButtonStyle);
                }
                else
                {
                    Log("Safari View not supported, falling back to default behavior");
                    Application.OpenURL(url);
                }
            }
            else
            #endif
            {
                Log("Platform neither Android nor iOS, falling back to default behavior");
                Application.OpenURL(url);
            }
        }

        public static void OpenWithDefaultSettings(string url)
        {
            var instance = new BetterOpenURL();
            instance.Open(url);
        }
        
        private void Log(string l)
        {
            if (Logging) Debug.Log($"{nameof(BetterOpenURL)}: {l}");
        }
    }
}

#if UNITY_IOS
using System.Runtime.InteropServices;
using AOT;
using TimboJimbo.BetterOpenURL.Auth;
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

        [DllImport("__Internal")]
        private static extern void BetterOpenURL_startAuthentication(
            string url,
            string schema,
            StartAuthenticationCallbackHelper.CallbackDelegate callback
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
        
        public static void StartAuthentication(string url, string schema, OnAuthResult onAuthResult)
        {
            StartAuthenticationCallbackHelper.ActiveOnAuthResult = onAuthResult;
            BetterOpenURL_startAuthentication(url, schema, StartAuthenticationCallbackHelper.Handler);
        }

        /// <summary>
        /// IL2CPP Does not support marshalling instance delegates to native code, so we need to use static delegates as a workaround.
        /// </summary>
        private static class StartAuthenticationCallbackHelper
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void CallbackDelegate(int result, string url);
            
            public static OnAuthResult ActiveOnAuthResult;
            
            [MonoPInvokeCallback(typeof(CallbackDelegate))]
            public static void Handler(int result, string resultUrl)
            {
                ActiveOnAuthResult?.Invoke((AuthURLResult)result, resultUrl);
                ActiveOnAuthResult = null;
            }
        }
        
    }
}
#endif
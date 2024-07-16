#if UNITY_ANDROID
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.Android
{
    internal class AndroidBindings
    {
        private static bool _isInitialized;
        private static AndroidJavaObject _context;
        private static AndroidJavaClass _betterOpenURLClass;
        private static AndroidJavaClass _applicationAnimationEnum;
        private static AndroidJavaClass _customTabAnimationEnum;
        public static void TestLog()
        {
            Init();
            _betterOpenURLClass.CallStatic("testLog");
        }

        public static bool HasCustomTabsSupport()
        {
            Init();
            return _betterOpenURLClass.CallStatic<bool>("hasCustomTabsSupport", _context);
        }

        public static void OpenInCustomTab(string url, bool customToolbarColors, Color toolbarColor, Color toolbarSecondaryColor, bool showTitle, bool urlBarHidingEnabled, bool customAnimations, AndroidApplicationAnimation applicationAnimation, AndroidCustomTabAnimations customTabAnimation)
        {
            Init();
            var applicationAnimationEnumValue = _applicationAnimationEnum.GetStatic<AndroidJavaObject>(LowerFirst(applicationAnimation.ToString()));
            var customTabAnimationEnumValue = _customTabAnimationEnum.GetStatic<AndroidJavaObject>(LowerFirst(customTabAnimation.ToString()));
            _betterOpenURLClass.CallStatic(
                "openInCustomTab", 
                _context, 
                url,
                customToolbarColors,
                $"#{ColorUtility.ToHtmlStringRGB(toolbarColor)}",
                $"#{ColorUtility.ToHtmlStringRGB(toolbarSecondaryColor)}", 
                showTitle, 
                urlBarHidingEnabled, 
                customAnimations, 
                applicationAnimationEnumValue, 
                customTabAnimationEnumValue
            );
            
            string LowerFirst(string s) => char.ToLower(s[0]) + s[1..];
        }

        private static void Init()
        {
            if(_isInitialized) return;

            _isInitialized = true;

            using (var UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                _context = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

            _betterOpenURLClass = new AndroidJavaClass("com.timbojimbo.betteropenurl.BetterOpenURL");
            _applicationAnimationEnum = new AndroidJavaClass("com.timbojimbo.betteropenurl.ApplicationAnimation");
            _customTabAnimationEnum = new AndroidJavaClass("com.timbojimbo.betteropenurl.CustomTabAnimation");
        }
    }
}
#endif
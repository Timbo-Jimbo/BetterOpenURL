using System;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.Android
{
    [Serializable]
    public class AndroidSettings
    {
        public bool CustomToolbarColors;
        [ShowIf(nameof(CustomToolbarColors))]
        [ColorUsage(false)]
        public Color ToolbarColor = Color.white;
        [ShowIf(nameof(CustomToolbarColors))]
        [ColorUsage(false)]
        public Color ToolbarSecondaryColor= Color.white;
        public bool ShowTitle = true;
        public bool UrlBarHidingEnabled = true;
        public bool CustomAnimations;
        [ShowIf(nameof(CustomAnimations))]
        public AndroidApplicationAnimation ApplicationAnimation = AndroidApplicationAnimation.FallAway;
        [ShowIf(nameof(CustomAnimations))]
        public AndroidCustomTabAnimations CustomTabAnimation = AndroidCustomTabAnimations.OpenFromBottom;
    }
}
using System;
using TimboJimbo.BetterOpenURL.Android;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.iOS
{
    [Serializable]
    public class iOSSettings
    {
        public bool CustomColor;
        [ShowIf(nameof(CustomColor))]
        [ColorUsage(false)]
        public Color BarTintColor = Color.white;
        public bool BarCollapsingEnabled = true;
        public iOSTransitionStyle TransitionStyle = iOSTransitionStyle.CoverOver;
        public iOSPresentationStyle PresentationStyle = iOSPresentationStyle.FullScreen;
        public iOSDismissButtonStyle DismissButtonStyle = iOSDismissButtonStyle.Done;
    }
}
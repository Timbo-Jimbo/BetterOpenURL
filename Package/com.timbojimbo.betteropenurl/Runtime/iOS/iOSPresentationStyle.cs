using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maps to UIKit.UIViewController.UIModalPresentationStyle enum
/// </summary>
public enum iOSPresentationStyle : int
{
    FullScreen = 0,
    PageSheet = 1,
    FormSheet = 2,
    CurrentContext = 3,
    OverFullScreen = 5,
    OverCurrentContext = 6,
    Popover = 7
}

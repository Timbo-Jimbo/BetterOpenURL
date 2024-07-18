//
//  BetterOpenURLBridging.swift
//  BetterOpenURL
//
//  Created by Timbo Jimbo on 16/7/2024.
//

import Foundation
import SafariServices
import UIKit

@_cdecl("BetterOpenURL_supportsSafariView")
public func BetterOpenURL_supportsSafariView() -> Bool
{
    return supportsSafariView();
}

@_cdecl("BetterOpenURL_openSafariView")
public func BetterOpenURL_openSafariView(
    url: UnsafePointer<CChar>,
    customColor: Bool,
    barTintColorHex: UnsafePointer<CChar>,
    modalTransitionStyle: Int,
    modalPresentationStyle: Int,
    barCollapsingEnabled: Bool,
    dismissButtonStyle: Int
)
{
    openSafariView(
        url: String(cString: url),
        customColor: customColor,
        barTintColor: customColor ? (UIColor(hexRgb: String(cString: barTintColorHex)) ?? UIColor(white: 1, alpha: 1)) : UIColor(white: 1, alpha: 1),
        modalTransitionStyle: UIModalTransitionStyle(rawValue: modalTransitionStyle) ?? .coverVertical,
        modalPresentationStyle: UIModalPresentationStyle(rawValue: modalPresentationStyle) ?? .fullScreen,
        barCollapsingEnabled: barCollapsingEnabled,
        dismissButtonStyle: SFSafariViewController.DismissButtonStyle(rawValue: dismissButtonStyle) ?? .done
    )
}

@_cdecl("BetterOpenURL_startAuthentication")
public func BetterOpenURL_startAuthentication(
    url: UnsafePointer<CChar>,
    callbackUrlSchema: UnsafePointer<CChar>,
    handler: @convention(c) @escaping (Int, UnsafePointer<CChar>)-> Void
)
{
    let urlString = String(cString: url);
    startAuthentication(url: urlString, callbackUrlSchema: String(cString: callbackUrlSchema))
    { authViewResult, resultURL in
        
        print("startAuthentication completed " + (authViewResult == .completed ? "successfully" : "unsuccessfully...") + " for URL " + urlString);
        print("Invokign Unity callback");
        handler(authViewResult.rawValue, NSString(string: resultURL?.absoluteString ?? "").utf8String!);
    }
}

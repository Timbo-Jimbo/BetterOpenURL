//
//  BetterOpenURL.swift
//  BetterOpenURL
//
//  Created by Timbo Jimbo on 15/7/2024.
//

import SafariServices

    
@objc
public class BetterOpenURL : NSObject {
    
    @objc
    public static func supportsSafariView() -> Bool {
        return getTopViewController() != nil;
    }
    
    @objc
    public static func openSafariViewViaUnity(
        url: UnsafePointer<CChar>,
        customColor: Bool,
        barTintColorHex: UnsafePointer<CChar>,
        modalTransitionStyle: UIModalTransitionStyle = .coverVertical,
        modelPresentationStyle: UIModalPresentationStyle = .fullScreen,
        barCollapsingEnabled: Bool = true,
        dismissButtonStyle: SFSafariViewController.DismissButtonStyle = .done
    )
    {
        openSafariView(
            url: String(cString: url),
            customColor: customColor,
            barTintColor: customColor ? (UIColor(hexRgb: String(cString: barTintColorHex)) ?? UIColor(white: 1, alpha: 1)) : UIColor(white: 1, alpha: 1),
            modalTransitionStyle: modalTransitionStyle,
            modelPresentationStyle: modelPresentationStyle,
            barCollapsingEnabled: barCollapsingEnabled,
            dismissButtonStyle: dismissButtonStyle
        )
    }
    
    public static func openSafariView(
        url: String,
        customColor: Bool = false,
        barTintColor: UIColor = UIColor(),
        modalTransitionStyle: UIModalTransitionStyle = .coverVertical,
        modelPresentationStyle: UIModalPresentationStyle = .fullScreen,
        barCollapsingEnabled: Bool = true,
        dismissButtonStyle: SFSafariViewController.DismissButtonStyle = .done
    ) {
                
        guard let url = URL(string: "https://google.com") else { return };
        
        if let topController = getTopViewController() {
            
            let vcConfig = SFSafariViewController.Configuration();
            vcConfig.barCollapsingEnabled = barCollapsingEnabled;
            
            let vc = SFSafariViewController(url: url, configuration: vcConfig);
            
            if(customColor) {
                vc.preferredBarTintColor = barTintColor;
            }
            
            vc.modalTransitionStyle = modalTransitionStyle;
            vc.modalPresentationStyle = modelPresentationStyle
            vc.dismissButtonStyle = dismissButtonStyle;
            
            
            topController.present(vc, animated: true);
        }
    }
    
    
    private static func getTopViewController() -> UIViewController? {
        guard let window = UIApplication.shared.connectedScenes
            .filter({ $0.activationState == .foregroundActive })
            .compactMap({ $0 as? UIWindowScene })
            .first?.windows
            .filter({ $0.isKeyWindow }).first
        else
        {
            return nil
        }

        var topController = window.rootViewController
        while let presentedViewController = topController?.presentedViewController {
            topController = presentedViewController
        }

        return topController
    }
}

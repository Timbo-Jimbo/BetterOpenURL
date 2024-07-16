//
//  BetterOpenURL.swift
//  BetterOpenURL
//
//  Created by Timbo Jimbo on 15/7/2024.
//

import SafariServices

    
public func supportsSafariView() -> Bool {
    return getTopViewController() != nil;
}

public func openSafariView(
    url: String,
    customColor: Bool = false,
    barTintColor: UIColor = UIColor(),
    modalTransitionStyle: UIModalTransitionStyle = .coverVertical,
    modalPresentationStyle: UIModalPresentationStyle = .fullScreen,
    barCollapsingEnabled: Bool = true,
    dismissButtonStyle: SFSafariViewController.DismissButtonStyle = .done
) {
            
    guard let parsedUrl = URL(string: url) else { return };
    
    if let topController = getTopViewController() {
        
        let vcConfig = SFSafariViewController.Configuration();
        vcConfig.barCollapsingEnabled = barCollapsingEnabled;
        
        let vc = SFSafariViewController(url: parsedUrl, configuration: vcConfig);
        
        if(customColor) {
            vc.preferredBarTintColor = barTintColor;
        }
        
        vc.modalTransitionStyle = modalTransitionStyle;
        vc.modalPresentationStyle = modalPresentationStyle
        vc.dismissButtonStyle = dismissButtonStyle;
        
        
        topController.present(vc, animated: true);
    }
}


private func getTopViewController() -> UIViewController? {
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


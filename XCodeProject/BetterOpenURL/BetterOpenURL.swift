//
//  BetterOpenURL.swift
//  BetterOpenURL
//
//  Created by Timbo Jimbo on 15/7/2024.
//

import SafariServices
import AuthenticationServices;

public enum AuthViewResult : Int
{
    case completed = 0
    case failed = 1
}

public typealias AuthCompletionHandler = (AuthViewResult, URL?) -> Void

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
    print("openSafariView called for URL " + url);
    
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
        
        
        print("openSafariView presenting SFSafariViewController");
        topController.present(vc, animated: true);
    }
    else
    {
        print("openSafariView unable to locate top view controller, so we wont be able to show SafariView..!");
    }
}

public func startAuthentication(
    url: String,
    callbackUrlSchema: String,
    completionHandler: @escaping AuthCompletionHandler
) {
    if(ContextProvider.activeSessionHandle != nil)
    {
        print("startAuthentication called but we already have an active ASWebAuthenticationSession...? This will probably cause the other session to close.");
        ContextProvider.activeSessionHandle = nil;
    }
    
    print("startAuthentication called for URL " + url + " with Callback Schema set to " + callbackUrlSchema);
    
    // Use the URL and callback scheme specified by the authorization provider.
    guard let authURL = URL(string: url) else { return }

    // Initialize the session.
    let session = ASWebAuthenticationSession(url: authURL, callbackURLScheme: callbackUrlSchema)
    { callbackURL, error in
        print("ASWebAuthenticationSession completion handler was called");
        if(error != nil)
        {
            print("ASWebAuthenticationSession completion handler was called with an error..!");
            print(error?.localizedDescription as Any);
            completionHandler(.failed, callbackURL);
        }
        else
        {
            print("ASWebAuthenticationSession completion handler was called");
            completionHandler(.completed, callbackURL);
        }
        
        ContextProvider.activeSessionHandle = nil;
    }
    
    session.presentationContextProvider = ContextProvider.instance;
    
    let started = session.start();
    
    if(!started)
    {
        print("Was not able to start ASWebAuthenticationSession...");
        completionHandler(.failed, nil)
    }
    else
    {
        print("ASWebAuthenticationSession session started!");
        
        //We need to hold on to the instance so it doesnt get deallocated immedeately
        ContextProvider.activeSessionHandle = session;
    }
}

private class ContextProvider : NSObject, ASWebAuthenticationPresentationContextProviding {
    
    static var activeSessionHandle:ASWebAuthenticationSession? = nil;
    static let instance: ContextProvider = ContextProvider();
    
    @available(iOS 13.0, *)
    func presentationAnchor(for session: ASWebAuthenticationSession) -> ASPresentationAnchor {
        return getTopWindow()!;
    }
}

private func getTopWindow() -> UIWindow? {
    return UIApplication.shared.connectedScenes
        .filter({ $0.activationState == .foregroundActive })
        .compactMap({ $0 as? UIWindowScene })
        .first?.windows
        .filter({ $0.isKeyWindow }).first
}

private func getTopViewController() -> UIViewController? {
    guard let window = getTopWindow()
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

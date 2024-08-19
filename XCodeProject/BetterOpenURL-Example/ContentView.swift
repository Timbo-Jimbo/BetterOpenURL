//
//  ContentView.swift
//  BetterOpenURL-Example
//
//  Created by Timbo Jimbo on 15/7/2024.
//

import SwiftUI
import BetterOpenURL;
import SafariServices;

struct ContentView: View {
    
    @State private var selectedPresentationStyle: UIModalPresentationStyle = .fullScreen;
    
    let presentationStyles = [
        UIModalPresentationStyle.fullScreen,
        UIModalPresentationStyle.pageSheet,
        UIModalPresentationStyle.formSheet,
        UIModalPresentationStyle.currentContext,
        UIModalPresentationStyle.overFullScreen,
        UIModalPresentationStyle.overCurrentContext,
        UIModalPresentationStyle.popover
    ]
    
    let presentationStyleStrings = [
        "fullScreen",
        "pageSheet",
        "formSheet",
        "currentContext",
        "overFullScreen",
        "overCurrentContext",
        "popover"
    ]
    
    @State private var selectedDismissButtonStyle: SFSafariViewController.DismissButtonStyle = .done;
    
    let dismissButtonStyles = [
        SFSafariViewController.DismissButtonStyle.cancel,
        SFSafariViewController.DismissButtonStyle.close,
        SFSafariViewController.DismissButtonStyle.done
    ]
    
    let dismissButtonStyleStrings = [
        "cancel",
        "close",
        "done"
    ]
    
    @State private var selectedTransitionStyle: UIModalTransitionStyle = .coverVertical;
    
    let transitionStyles = [
        UIModalTransitionStyle.coverVertical,
        UIModalTransitionStyle.flipHorizontal,
        UIModalTransitionStyle.crossDissolve
    ]
    
    let transitionStyleStrings = [
        "coverVertical",
        "flipHorizontal",
        "crossDissolve"
    ]
        
    @State private var barTintColor: Color = Color.white;
    
    var body: some View {
        VStack {
            LabeledContent("Transition Style") {
                Picker("Transition Style", selection: $selectedTransitionStyle) {
                    ForEach(transitionStyles, id: \.self) { value in
                        Text(transitionStyleStrings[transitionStyles.firstIndex(of: value) ?? 0])
                    }
                }
            }
            LabeledContent("Presentation Style") {
                Picker("Presentation Style", selection: $selectedPresentationStyle) {
                    ForEach(presentationStyles, id: \.self) { value in
                        Text(presentationStyleStrings[presentationStyles.firstIndex(of: value) ?? 0])
                    }
                }
            }
            LabeledContent("Dismiss Button Style") {
                Picker("Dismiss Button Style", selection: $selectedDismissButtonStyle) {
                    ForEach(dismissButtonStyles, id: \.self) { value in
                        Text(dismissButtonStyleStrings[dismissButtonStyles.firstIndex(of: value) ?? 0])
                    }
                }
            }
            ColorPicker("Tint Color", selection: $barTintColor, supportsOpacity: false)
            Button(
                action: {
                    openSafariView(
                        url: "https://google.com",
                        customColor: true,
                        barTintColor: UIColor(barTintColor),
                        modalTransitionStyle: selectedTransitionStyle,
                        modalPresentationStyle: selectedPresentationStyle,
                        dismissButtonStyle: selectedDismissButtonStyle
                    )
                }, label: {
                    Text("Open Link")
                }
            )
        }
        .padding()
    }
}

#Preview {
    ContentView()
}

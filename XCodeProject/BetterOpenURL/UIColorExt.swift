//
//  UIColorExt.swift
//  BetterOpenURL
//
//  Created by Timbo Jimbo on 15/7/2024.
//

import UIKit

internal extension UIColor {
    convenience init?(hexRgb: String) {
        let r, g, b: CGFloat

        if hexRgb.hasPrefix("#") {
            let start = hexRgb.index(hexRgb.startIndex, offsetBy: 1)
            let hexColor = String(hexRgb[start...])

            if hexColor.count == 6 {
                let scanner = Scanner(string: hexColor)
                var hexNumber: UInt64 = 0

                if scanner.scanHexInt64(&hexNumber) {
                    r = CGFloat((hexNumber & 0xff0000) >> 16) / 255
                    g = CGFloat((hexNumber & 0x00ff00) >> 8) / 255
                    b = CGFloat(hexNumber & 0x0000ff) / 255

                    self.init(red: r, green: g, blue: b, alpha: 1.0)
                    return
                }
            }
        }

        return nil
    }
}

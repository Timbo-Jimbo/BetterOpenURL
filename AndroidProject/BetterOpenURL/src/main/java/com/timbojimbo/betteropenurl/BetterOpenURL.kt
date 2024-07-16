package com.timbojimbo.betteropenurl

import android.content.Context
import android.graphics.Color
import android.net.Uri
import androidx.browser.customtabs.CustomTabColorSchemeParams
import androidx.browser.customtabs.CustomTabsClient
import androidx.browser.customtabs.CustomTabsIntent

@Suppress("MemberVisibilityCanBePrivate")
class BetterOpenURL {

    companion object {

        @JvmStatic
        fun openInCustomTab(context: Context, urlToLaunch: String, customColors: Boolean = false, toolbarColorHex: String = "", secondaryToolbarColorHex: String = "", showTitle: Boolean = true, urlBarHidingEnabled: Boolean = true, customAnimations: Boolean = true, applicationAnim: ApplicationAnimation = ApplicationAnimation.fallAway, customTabAnim: CustomTabAnimation = CustomTabAnimation.openFromBottom) {

            if(!hasCustomTabsSupport(context))
            {
                System.err.println("This device does not support Custom Tabs - you can call hasCustomTabsSupport to check if it is supported first!")
                return
            }

            val customTabsBuilder = CustomTabsIntent.Builder()
                .setShowTitle(showTitle)
                .setUrlBarHidingEnabled(urlBarHidingEnabled);

            if(customAnimations)
            {
                val applicationAnimSet = ApplicationAnimSets.get(applicationAnim);
                val customTabAnimSet = CustomTabAnimSets.get(customTabAnim);

                customTabsBuilder
                    .setStartAnimations(context, customTabAnimSet.firstAnim, applicationAnimSet.firstAnim)
                    .setExitAnimations(context, applicationAnimSet.secondAnim, customTabAnimSet.secondAnim)
            }

            if(customColors)
            {
                val colorSchemeParamsBuilder = CustomTabColorSchemeParams.Builder()

                if (toolbarColorHex.isNotEmpty())
                    colorSchemeParamsBuilder.setToolbarColor(Color.parseColor(toolbarColorHex))
                if (secondaryToolbarColorHex.isNotEmpty())
                    colorSchemeParamsBuilder.setSecondaryToolbarColor(Color.parseColor(secondaryToolbarColorHex))

                val defaultColorSchema = colorSchemeParamsBuilder.build()

                customTabsBuilder.setDefaultColorSchemeParams(defaultColorSchema)
            }

            customTabsBuilder.build().launchUrl(context, Uri.parse(urlToLaunch))
        }

        @JvmStatic
        fun hasCustomTabsSupport(context: Context): Boolean {

            val packageName = CustomTabsClient.getPackageName(
                context,
                emptyList<String>()
            )

            if(packageName != null)
            {
                println("Custom Tabs is supported and will be handled by: $packageName")
                return true
            }
            else
            {
                println("No browsers on this device support Custom Tabs!")
                return false
            }
        }

        @JvmStatic
        fun testLog() {
            println("This is a test...!!!");
        }
    }
}
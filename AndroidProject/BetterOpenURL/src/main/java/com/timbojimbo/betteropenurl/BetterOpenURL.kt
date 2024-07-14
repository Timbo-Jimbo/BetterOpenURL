package com.timbojimbo.betteropenurl

import android.content.Context
import android.graphics.Color
import android.net.Uri
import androidx.browser.customtabs.CustomTabColorSchemeParams
import androidx.browser.customtabs.CustomTabsClient
import androidx.browser.customtabs.CustomTabsIntent

internal class AnimSet(val firstAnim: Int, val secondAnim: Int)

internal class CustomTabAnimSets
{
    companion object {
        val pullIn = AnimSet(R.anim.pull_from_back, R.anim.push_to_back)
        val fallIn = AnimSet(R.anim.push_from_front, R.anim.pull_to_front)
        val openFromBottom = AnimSet(R.anim.open_from_bottom, R.anim.close_to_bottom)

        fun get(enumVal: CustomTabAnimations) : AnimSet
        {
            return when (enumVal) {
                CustomTabAnimations.pullIn -> pullIn
                CustomTabAnimations.fallIn -> fallIn
                CustomTabAnimations.openFromBottom -> openFromBottom
            }
        }
    }
}

internal class ApplicationAnimSets
{
    companion object {
        val fallAway = AnimSet(R.anim.push_to_back, R.anim.pull_from_back)
        val flyAway = AnimSet(R.anim.pull_to_front, R.anim.push_from_front)
        val closeToBottom = AnimSet(R.anim.close_to_bottom, R.anim.open_from_bottom)
        var crouchDown = AnimSet(R.anim.crouch_down, R.anim.stand_up)

        fun get(enumVal: ApplicationAnimation) : AnimSet
        {
            return when (enumVal) {
                ApplicationAnimation.fallAway -> fallAway
                ApplicationAnimation.flyAway -> flyAway
                ApplicationAnimation.closeToBottom -> closeToBottom
                ApplicationAnimation.crouchDown -> crouchDown
            }
        }
    }
}

enum class ApplicationAnimation
{
    fallAway,
    flyAway,
    closeToBottom,
    crouchDown,
}

enum class CustomTabAnimations
{
    pullIn,
    fallIn,
    openFromBottom
}

class BetterOpenURL {

    companion object {

        fun openInCustomTab(context: Context, urlToLaunch: String, toolbarColorHex: String = "", secondaryToolbarColorHex: String = "", showTitle: Boolean = true, urlBarHidingEnabled: Boolean = true, applicationAnim: ApplicationAnimation = ApplicationAnimation.fallAway, customTabAnim: CustomTabAnimations = CustomTabAnimations.openFromBottom) {

            if(!hasCustomTabsSupport(context))
            {
                System.err.println("This device does not support Custom Tabs - you can call hasCustomTabsSupport to check if it is supported first!")
                return
            }

            val colorSchemeParamsBuilder = CustomTabColorSchemeParams.Builder()

            if (toolbarColorHex.isNotEmpty())
                colorSchemeParamsBuilder.setToolbarColor(Color.parseColor(toolbarColorHex))
            if (secondaryToolbarColorHex.isNotEmpty())
                colorSchemeParamsBuilder.setSecondaryToolbarColor(Color.parseColor(secondaryToolbarColorHex))

            val defaultColorSchema = colorSchemeParamsBuilder.build()

            val applicationAnimSet = ApplicationAnimSets.get(applicationAnim);
            val customTabAnimSet = CustomTabAnimSets.get(customTabAnim);

            val customTabs = CustomTabsIntent.Builder()
                .setDefaultColorSchemeParams(defaultColorSchema)
                .setShowTitle(showTitle)
                .setUrlBarHidingEnabled(urlBarHidingEnabled)
                .setStartAnimations(context, customTabAnimSet.firstAnim, applicationAnimSet.firstAnim)
                .setExitAnimations(context, applicationAnimSet.secondAnim, customTabAnimSet.secondAnim)
                .build()

            customTabs.launchUrl(context, Uri.parse(urlToLaunch))
        }

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
    }
}
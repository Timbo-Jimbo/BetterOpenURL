package com.timbojimbo.betteropenurl

import android.content.Context
import android.graphics.Color
import android.net.Uri
import androidx.browser.customtabs.CustomTabColorSchemeParams
import androidx.browser.customtabs.CustomTabsClient
import androidx.browser.customtabs.CustomTabsIntent


class BetterOpenURL {

    companion object {
        fun open(context: Context, urlToLaunch: String, toolbarColorHex: String = "", secondaryToolbarColorHex: String = "", showTitle: Boolean = true, urlBarHidingEnabled: Boolean = true) {

            val colorSchemeParamsBuilder = CustomTabColorSchemeParams.Builder()

            if (toolbarColorHex.isNotEmpty())
                colorSchemeParamsBuilder.setToolbarColor(Color.parseColor(toolbarColorHex))
            if (secondaryToolbarColorHex.isNotEmpty())
                colorSchemeParamsBuilder.setSecondaryToolbarColor(Color.parseColor(secondaryToolbarColorHex))

            val defaultColorSchema = colorSchemeParamsBuilder.build()

            val customTabs = CustomTabsIntent.Builder()
                .setDefaultColorSchemeParams(defaultColorSchema)
                .setShowTitle(showTitle)
                .setUrlBarHidingEnabled(urlBarHidingEnabled)
                .build();

            customTabs.launchUrl(context, Uri.parse(urlToLaunch))
        }

        fun hasCustomTabsSupport(context: Context): Boolean {
            val packageName = CustomTabsClient.getPackageName(
                context,
                emptyList<String>()
            )

            return packageName != null;
        }
    }
}
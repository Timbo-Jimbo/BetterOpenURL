package com.timbojimbo.betteropenurl_example

import android.os.Bundle
import android.widget.AdapterView.OnItemSelectedListener
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.Spinner
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.timbojimbo.betteropenurl.ApplicationAnimation
import com.timbojimbo.betteropenurl.BetterOpenURL
import com.timbojimbo.betteropenurl.CustomTabAnimations

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_main)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        val applicationAnimAdaptor = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, ApplicationAnimation.entries);
        val applicationAnimationSpinner = this.findViewById<Spinner>(R.id.application_anim);
        applicationAnimationSpinner.adapter = applicationAnimAdaptor;
        applicationAnimationSpinner.setSelection(ApplicationAnimation.entries.indexOf(ApplicationAnimation.fallAway));

        val customTabAnimAdaptor = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, CustomTabAnimations.entries);
        val customTabsAnimSpinner = this.findViewById<Spinner>(R.id.custom_tab_anim);
        customTabsAnimSpinner.adapter = customTabAnimAdaptor;
        customTabsAnimSpinner.setSelection(CustomTabAnimations.entries.indexOf(CustomTabAnimations.openFromBottom));

        val launchButton = this.findViewById<Button>(R.id.launch)
        launchButton.setOnClickListener {
            BetterOpenURL.openInCustomTab(
                this,
                "https://google.com",
                applicationAnim = applicationAnimationSpinner.selectedItem as ApplicationAnimation,
                customTabAnim = customTabsAnimSpinner.selectedItem as CustomTabAnimations
            )
        }

    }
}
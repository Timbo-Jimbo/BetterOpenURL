plugins {
    alias(libs.plugins.android.library)
    alias(libs.plugins.jetbrains.kotlin.android)
}

android {
    namespace = "com.timbojimbo.betteropenurl"
    compileSdk = 34

    defaultConfig {
        minSdk = 22
        consumerProguardFiles("consumer-rules.pro")
    }

    buildTypes {
        release {
            isDefault = true
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
    kotlinOptions {
        jvmTarget = "1.8"
    }
}

// Register the copyAAR task
val copyAAR by tasks.register<Copy>("copyAAR") {
    val buildDir = layout.buildDirectory.asFile.get().absolutePath
    val packageDir = layout.projectDirectory.asFile.absolutePath + "/../../Package/com.timbojimbo.betteropenurl"

    from("$buildDir/outputs/aar/BetterOpenURL-release.aar")
    into("$packageDir/Plugins/Android/")
    doLast {
        println("Copied AAR to Unity project")
    }
}

// Ensure copyAAR runs after assembleRelease
afterEvaluate {
    tasks.named("assembleRelease") {
        finalizedBy(copyAAR)
    }
}


dependencies {
    implementation(libs.androidx.core.ktx)
    implementation(libs.androidx.appcompat)
    implementation(libs.material)
    implementation(libs.androidx.browser.v160)
}
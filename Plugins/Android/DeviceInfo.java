package io.github.idreamsofgame.unisharper.core;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Build;
import android.provider.Settings;

import com.unity3d.player.UnityPlayer;

/** This class encapsulates information about Android device. */
public final class DeviceInfo {

    /**
     * Gets a unique device identifier of this Android device.
     *
     * @return A unique device identifier.
     */
    public static String getUniqueDeviceIdentifier() {
        Context context = UnityPlayer.currentActivity;

        @SuppressLint("HardwareIds")
        String androidID = Settings.Secure.getString(context.getContentResolver(), Settings.Secure.ANDROID_ID);
        String serialNumber = Build.SERIAL;
        return androidID + "-" + serialNumber;
    }
}

package io.github.idreamsofgame.unisharper.plugin;

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
        @SuppressLint("HardwareIds")
        String serialNumber = Build.SERIAL;
        return androidID + "-" + serialNumber;
    }

    /**
     * Gets the country code of this Android device.
     *
     * @return The country/region code, which should either be the empty string, an uppercase ISO 3166 2-letter code, or a UN M.49 3-digit code.
     */
    public static String getCountryCode() {
        Context context = UnityPlayer.currentActivity;
        String countryCode;

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            countryCode = context.getResources().getConfiguration().getLocales().get(0).getCountry();
        }
        else {
            countryCode = context.getResources().getConfiguration().locale.getCountry();
        }

        return countryCode;
    }
}

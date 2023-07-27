package io.github.idreamsofgame.unisharper.plugin;

import android.content.Context;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

/** This class provide static functions to use Android widgets. */
public final class WidgetUtils {

    /**
     * Show native toast message of Android.
     * @param message
     * @param longDuration
     */
    public static void showToast(String message, boolean longDuration) {
        Context context = UnityPlayer.currentActivity;
        Toast.makeText(context, message, longDuration ? Toast.LENGTH_LONG : Toast.LENGTH_SHORT).show();
    }
}

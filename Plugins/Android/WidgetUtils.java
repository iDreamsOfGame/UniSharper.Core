package io.github.idreamsofgame.unisharper.core;

import android.content.Context;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

/** This class provide static functions to use Android widgets. */
public final class WidgetUtils {

    /**
     * Show native toast message of Android.
     * @param message
     * @param showLong
     */
    public static void showToast(String message, boolean showLong) {
        Context context = UnityPlayer.currentActivity;
        Toast.makeText(context, message, showLong ? Toast.LENGTH_LONG : Toast.LENGTH_SHORT).show();
    }
}

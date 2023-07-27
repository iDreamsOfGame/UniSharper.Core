package io.github.idreamsofgame.unisharper.plugin;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;

import com.unity3d.player.UnityPlayer;

/** This class provide static functions to handle network operation by native code. */
public final class NetUtils {

    /**
     * Opens the URL specified.
     * @param url
     * @return
     */
    public static boolean openURL(String url) {
        try {
            Context context = UnityPlayer.currentActivity;
            context.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(url)));
            return true;
        } catch (Exception exception) {
            return false;
        }
    }
}

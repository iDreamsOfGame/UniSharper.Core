package io.github.idreamsofgame.unisharper.plugin;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

/** This class encapsulates information about Android app. */
public final class AppUtils {

    private static final String TAG = "AppUtils";

    /**
     * Quit the app.
     * @param killProcess killProcess whether to kill the process forcibly.
     */
    public static void quit(boolean killProcess) {
        try {
            Activity context = UnityPlayer.currentActivity;
            if (killProcess) {
                context.finishAffinity();
                System.exit(0);
            } else {
                context.moveTaskToBack(true);
                context.finishAffinity();
            }
        } catch (Exception exception) {
            Log.e(TAG, "Error on quitting app", exception);
        }
    }

    /**
     * Restart the app.
     */
    public static void restart() {
        try {
            // Restart app
            Activity context = UnityPlayer.currentActivity;
            PackageManager packageManager = context.getPackageManager();
            Intent intent = packageManager.getLaunchIntentForPackage(context.getPackageName());
            assert intent != null;
            intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
            context.startActivity(intent);
            context.finishAffinity();

            // Kill app
            int myPid = android.os.Process.myPid();
            android.os.Process.killProcess(myPid);
        } catch (Exception exception) {
            Log.e(TAG, "Error on restarting app", exception);
        }
    }
}

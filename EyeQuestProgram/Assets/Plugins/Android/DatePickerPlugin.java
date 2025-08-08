package com.example.datepicker;
import android.util.Log;
import android.app.DatePickerDialog;
import android.widget.DatePicker;
import android.app.Activity;

import com.unity3d.player.UnityPlayer;

import java.util.Calendar;

public class DatePickerPlugin {

    public static void ShowDatePicker(final String gameObjectName, final String callbackMethod) {
        final Activity activity = UnityPlayer.currentActivity;

        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Calendar c = Calendar.getInstance();
                int year = c.get(Calendar.YEAR);
                int month = c.get(Calendar.MONTH);
                int day = c.get(Calendar.DAY_OF_MONTH);

                DatePickerDialog dpd = new DatePickerDialog(activity,
                        new DatePickerDialog.OnDateSetListener() {
                            @Override
                            public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
                                String date = year + "-" + (monthOfYear + 1) + "-" + dayOfMonth;
                                UnityPlayer.UnitySendMessage(gameObjectName, callbackMethod, date);
                                Log.d("DatePickerPlugin", "Sending date to Unity: " + date);
                            }
                        }, year, month, day);
                dpd.show();
            }
        });
    }
}
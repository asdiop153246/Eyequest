using UnityEngine;
using System.Runtime.InteropServices;

public class NativeDatePicker : MonoBehaviour
{

    public void _Showing()
    {
        NativeDatePicker.Show("DatePickerManager", "OnDateSelected");

        Debug.Log("CLICK SHOW");
    }

#if UNITY_ANDROID
    public static void Show(string gameObjectName, string callbackMethod)
    {
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.example.datepicker.DatePickerPlugin"))
        {
            pluginClass.CallStatic("ShowDatePicker", gameObjectName, callbackMethod);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern void ShowDatePicker(string gameObjectName, string callbackMethod);

    public static void Show(string gameObjectName, string callbackMethod)
    {
        ShowDatePicker(gameObjectName, callbackMethod);
    }
#endif

    // Example callback
    public void OnDateSelected(string date)
    {
        Debug.Log("Selected date: " + date);

        _DateUI.text = date;
    }

    public TMPro.TextMeshProUGUI _DateUI;
}

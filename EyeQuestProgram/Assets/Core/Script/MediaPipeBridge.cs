using System.Runtime.InteropServices;
using UnityEngine;

public class MediaPipeBridge : MonoBehaviour
{
    // เรียก native function จาก mediapipe_jni.so
    [DllImport("mediapipe_jni")]
    private static extern void YourNativeFunction();  // ชื่อฟังก์ชันใน C/C++ ที่คุณต้องการเรียก

    void Start()
    {
        Debug.Log("Calling native function...");
        try
        {
            YourNativeFunction();
            Debug.Log("Native function called successfully.");
        }
        catch (System.DllNotFoundException e)
        {
            Debug.LogError("DllNotFoundException: " + e.Message);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Other Exception: " + ex.Message);
        }
    }


}
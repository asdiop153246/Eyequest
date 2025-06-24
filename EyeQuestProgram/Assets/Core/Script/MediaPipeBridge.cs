using System.Runtime.InteropServices;
using UnityEngine;

public class MediaPipeBridge : MonoBehaviour
{
    // เรียก native function จาก mediapipe_jni.so
    [DllImport("mediapipe_jni")]
    private static extern void YourNativeFunction();  // ชื่อฟังก์ชันใน C/C++ ที่คุณต้องการเรียก

    void Start()
    {
        // เรียกฟังก์ชัน native เมื่อเริ่มเกม
        Debug.Log("Calling native function...");
        YourNativeFunction();
    }
}
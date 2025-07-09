using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaturationController : MonoBehaviour
{
    public Volume volume; // อ้างอิง Global Volume หรือ Local Volume ที่ตั้งไว้ใน Scene

    public float targetGrayValue = 0f;
    public float currentGrayValue = 0f;
    public float lerpSpeed = 1f;

    public ColorAdjustments _temp;
    void Start()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            _temp = colorAdjustments;
        }

        if (volume == null)
        {
            Debug.LogError("กรุณาใส่ Volume ที่ต้องการควบคุม Saturation");
            return;
        }

        // ดึง ColorAdjustments จาก Volume Profile
        /*if (volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            // ตั้งค่า Saturation เป็น -100 (ขาวดำ)
            colorAdjustments.saturation.value = -100f;
        }
        else
        {
            Debug.LogWarning("Volume Profile ไม่มี Color Adjustments อยู่");
        }*/

        //Invoke("_DelayThisshit", 5);

       
    }

    // ตัวอย่างฟังก์ชันปรับค่า Saturation แบบไดนามิก
    public void SetSaturation(float value)
    {
        if (volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            colorAdjustments.saturation.value = Mathf.Clamp(value, -100f, 0f);
        }
    }

    public bool _Starter;

    public void StarterVolut()
    {
        if (_Starter)
        {
            _temp.saturation.value -= lerpSpeed;

            if(_temp.saturation.value <= -100f)
            {
                _Starter = false;
            }
        }
        
    }

    public void _isStarter()
    {
        _Starter = true;
    }

    public void FixedUpdate()
    {
        StarterVolut();
    }

}

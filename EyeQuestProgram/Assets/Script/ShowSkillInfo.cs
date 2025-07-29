using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillInfo : MonoBehaviour
{
    private float holdTime = 0f;
    [SerializeField] private float holdThreshold = 0.5f; // กำหนดเวลาที่จะนับเป็น Hold

    public GameObject _SkillInfo;
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        // ทดสอบด้วย Mouse
        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;

            if (holdTime >= holdThreshold)
            {
                _SkillInfo.SetActive(true);
                Debug.Log("Hold Detected (Mouse)");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Hold Released (Mouse), Hold Time: {holdTime:F2} วินาที");
            holdTime = 0;
            _SkillInfo.SetActive(false);
        }
#else
        // ใช้บนมือถือ
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                holdTime += Time.deltaTime;

                if (holdTime >= holdThreshold)
                {
                    Debug.Log("Hold Detected (Touch)");
                    _SkillInfo.SetActive(true);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Debug.Log($"Hold Released (Touch), Hold Time: {holdTime:F2} วินาที");
                holdTime = 0;
                _SkillInfo.SetActive(false);
            }
        }
#endif
    }
}

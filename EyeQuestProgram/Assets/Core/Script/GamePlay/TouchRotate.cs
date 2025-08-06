using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    private Vector2 lastMousePos;
    public FirstAction _Core;
    private Quaternion initialRotation;
    private Coroutine returnRotationCoroutine;
    public float returnDuration = 0.5f; // เวลาที่ใช้ในการหมุนกลับ

    public void _Reset()
    {
            // หยุด coroutine เดิมก่อนถ้ามี
            if (returnRotationCoroutine != null)
                StopCoroutine(returnRotationCoroutine);

            returnRotationCoroutine = StartCoroutine(RotateBackToInitial());
    }
    void Start()
    {
        initialRotation = transform.rotation;

        _Reset();
    }

    void Update()
    {
        // กดปุ่ม R เพื่อเริ่มการหมุนกลับแบบมี animation
        

        // Mouse input (Editor or Desktop)
        if (Input.GetMouseButtonDown(0))
        {
            _Core._CallFirstAction("Touch Kiwi");
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePos;
            float rotY = delta.x * rotationSpeed;

            transform.Rotate(0, rotY, 0, Space.World);
            lastMousePos = Input.mousePosition;
        }

        // Touch input (Mobile)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                _Core._CallFirstAction("Touch Kiwi");
                float rotY = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(0, rotY, 0, Space.World);
            }
        }
    }

    IEnumerator RotateBackToInitial()
    {
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;

        while (elapsed < returnDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, initialRotation, elapsed / returnDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation;
    }
}

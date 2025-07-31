using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    private Vector2 lastMousePos;
    public FirstAction _Core;

    void Update()
    {
        // Mouse input (Editor or Desktop)
        if (Input.GetMouseButtonDown(0))
        {
            _Core._CallFirstAction("Touch Kiki");
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
                _Core._CallFirstAction("Touch Kiki");
                float rotY = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(0, rotY, 0, Space.World);
            }
        }
    }
}

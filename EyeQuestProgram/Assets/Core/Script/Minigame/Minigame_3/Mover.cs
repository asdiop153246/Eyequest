using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1f; // UI speed (pixels per second)
    private float lifetime = 0.5f;
    private RectTransform rectTransform;
    public Minigame_3_core _Core;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public float distance;

    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        }

        if (_isPass)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                _Core.GetComponent<Minigame_3_core>()._PINStore.Remove(this.gameObject);
                Destroy(gameObject);
            }
        }
       


        Vector2 screenTarget = RectTransformUtility.WorldToScreenPoint(null, _Core.GetComponent<Minigame_3_core>()._UITarget.position);
        Vector2 screenMarker = RectTransformUtility.WorldToScreenPoint(null, this.gameObject.transform.position);
        distance = Vector2.Distance(screenTarget, screenMarker);

        if (distance <= 5)
        {
            _isPass = true;
        }
    }

    public bool _isPass;
}

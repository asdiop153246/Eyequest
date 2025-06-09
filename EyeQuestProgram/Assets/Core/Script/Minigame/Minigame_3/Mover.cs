using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1f; // UI speed (pixels per second)
    private float lifetime = 50f;
    private RectTransform rectTransform;
    public Minigame_3_core _Core;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        }

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            _Core.GetComponent<Minigame_3_core>()._PINStore.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}

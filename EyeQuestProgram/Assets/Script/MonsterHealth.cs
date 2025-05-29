using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [Header("Monster Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;

    [Header("Health Bar")]
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    void Lateupdate()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        transform.forward = cam.transform.forward;
        // transform.LookAt(cam.transform);
        // transform.Rotate(0, 180, 0); // Adjust rotation to face the camera correctly
    }
}

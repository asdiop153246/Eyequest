using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedRandomRotation : MonoBehaviour
{
    private Vector3 randomRotation;

    void Start()
    {
        // Random rotation speed per axis
        randomRotation = new Vector3(
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f)
        );
    }

    void Update()
    {
        transform.Rotate(randomRotation * Time.deltaTime);
    }
}

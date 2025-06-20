using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public List<AudioClip> _SFX;
    public void _PlaySFX(int _id)
    {
        GetComponent<AudioSource>().PlayOneShot(_SFX[_id]);
    }
}

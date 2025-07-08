using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable, VolumeComponentMenu("Custom/Grayscale Effect")]
public class GrayscaleEffect : VolumeComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(1f, 0f, 1f);
}

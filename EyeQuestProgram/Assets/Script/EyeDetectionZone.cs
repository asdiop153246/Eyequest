using Mediapipe.Unity;
using UnityEngine;

public class EyeDetectionZone : MonoBehaviour
{
  public int _eyeID;
  public GameObject _nextBox;

  private float eyeStayTimer = 0f;
  public float requiredStayTime = 2f;
  private bool isEyeInside = false;
  private bool hasTriggered = false;

  private void Update()
  {
    if (isEyeInside && !hasTriggered)
    {
      eyeStayTimer += Time.deltaTime;

      if (eyeStayTimer >= requiredStayTime)
      {
        hasTriggered = true;
        Debug.Log("Detect the Specific Eye");
        this.gameObject.SetActive(false);
        eyeStayTimer = 0f;
        if (_nextBox != null)
        {
          _nextBox.SetActive(true);
        }
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    PointAnnotation point = other.gameObject.GetComponent<PointAnnotation>();
    if (point != null && point._id == _eyeID)
    {
      isEyeInside = true;
      eyeStayTimer = 0f; // start counting
    }
  }

  private void OnTriggerExit(Collider other)
  {
    PointAnnotation point = other.gameObject.GetComponent<PointAnnotation>();
    if (point != null && point._id == _eyeID)
    {
      isEyeInside = false;
      eyeStayTimer = 0f; // reset timer on exit
    }
  }
}

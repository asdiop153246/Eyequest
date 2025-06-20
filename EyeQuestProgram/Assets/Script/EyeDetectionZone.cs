using Mediapipe.Unity;
using UnityEngine;
using UnityEngine.Events;
public class EyeDetectionZone : MonoBehaviour
{
  public int _eyeID;
  public GameObject _nextBox;
  public UnityEvent onEyeDetected;
  private float eyeStayTimer = 0f;
  public float requiredStayTime = 2f;
  private bool isEyeInside = false;

    private SFXManager _SFX;
  //private bool hasTriggered = false;
  private void OnEnable()
  {
      requiredStayTime = 0.2f;
      isEyeInside = false;
      eyeStayTimer = 0f;

      if (_nextBox != null)
      {
          _nextBox.SetActive(false); // Ensure the next box starts inactive
      }

        _SFX = FindObjectOfType<SFXManager>();

    }
    // Update is called once per frame
    private void Update()
    {
        if (isEyeInside)
        {
            eyeStayTimer += Time.deltaTime;

            if (eyeStayTimer >= requiredStayTime)
            {
                //Debug.Log("Detect the Specific Eye");
                this.gameObject.SetActive(false);
                _SFX._PlaySFX(0);
                isEyeInside = false;
                eyeStayTimer = 0f;
                onEyeDetected.Invoke();
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
    if (point != null)
    {
      isEyeInside = true;
      eyeStayTimer = 0f; // start counting
    }
  }

    /*private void OnTriggerStay(Collider other)
    {
        PointAnnotation point = other.gameObject.GetComponent<PointAnnotation>();
        if (point != null)
        {
            isEyeInside = true;
            //eyeStayTimer = 0f; // start counting
        }
    }*/

    private void OnTriggerExit(Collider other)
  {
    PointAnnotation point = other.gameObject.GetComponent<PointAnnotation>();
    if (point != null)
    {
      isEyeInside = false;
      eyeStayTimer = 0f; // reset timer on exit
    }
  }
}

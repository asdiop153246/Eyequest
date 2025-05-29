using UnityEngine;
using Mediapipe.Unity;
using TMPro;
public class BlinkDetectionScript : MonoBehaviour
{
  public GameObject _Toppoint;
  public GameObject _Bottompoint;
  public GameObject _blinkText;

  private float blinkTimer = 0f;
  private float holdBlinkThreshold = 2f;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    PointAnnotation[] points = GetComponentsInChildren<PointAnnotation>();
    foreach (var point in points)
    {
      //Debug.Log("Point ID: " + point._id);
      if (point != null && point._id == 374)
      {
        _Toppoint = point.gameObject;
        //Debug.Log("Right Iris Found: " + _rightIris.name);
      }
      else if (point != null && point._id == 386)
      {
        _Bottompoint = point.gameObject;
        //Debug.Log("Left Iris Found: " + _leftIris.name);
      }
      else if (_Toppoint != null && _Bottompoint != null)
      {
        Debug.Log("Both Point Found");
        break;
      }
    }
    
  }

  private void Update()
  {
    if (_blinkText == null)
    {
      _blinkText = GameObject.Find("BlinkText");
      if (_blinkText != null)
      {
        _blinkText.GetComponent<TextMeshProUGUI>().fontSize = 80;
        _blinkText.SetActive(false);
      }
    }

    if (_Toppoint != null && _Bottompoint != null && _blinkText != null)
    {
      Vector3 topLocalPosition = _Toppoint.transform.localPosition;
      Vector3 bottomLocalPosition = _Bottompoint.transform.localPosition;
      float distance = Vector3.Distance(topLocalPosition, bottomLocalPosition);

      if (distance < 13.5f)
      {
        blinkTimer += Time.deltaTime;

        _blinkText.SetActive(true);
        if (blinkTimer >= holdBlinkThreshold)
        {
          _blinkText.GetComponent<TextMeshProUGUI>().text = "Hold Blink";
        }
        else
        {
          _blinkText.GetComponent<TextMeshProUGUI>().text = "Blink";
        }
      }
      else
      {
        blinkTimer = 0f;
        _blinkText.SetActive(false);
      }
    }
  }
}

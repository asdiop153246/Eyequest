using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity;
using TMPro;
public class GetPointID : MonoBehaviour
{
  public GameObject _centerPoint;
  public bool _isCenterPoint = false;
  public GameObject _DetectionZone;
  public GameObject _centerText;
  public GameObject _CenterNoseAlignment;
  private void Start()
  {
    Debug.Log("Start Searching for CenterPoint");
    
    if (_CenterNoseAlignment == null)
    {
      _CenterNoseAlignment = GameObject.Find("CenterNoseAlignment");
      if (_CenterNoseAlignment != null)
      {
        _CenterNoseAlignment.SetActive(false);
      }
    }

    if (_centerText == null)
    {
      _centerText = GameObject.Find("CenterfaceText");
      if (_centerText != null)
      {
        _centerText.SetActive(false);
        _centerText.GetComponent<TextMeshProUGUI>().fontSize = 60;
      }

    }

    PointAnnotation[] points = GetComponentsInChildren<PointAnnotation>();

    foreach (var point in points)
    {
      if (point != null && point._id == 4)
      {
        _centerPoint = point.gameObject;
        _centerPoint.GetComponent<PointAnnotation>().SetRadius(1f);
        Debug.Log("CenterPoint found: " + _centerPoint.name);
      }
    }
    if (_DetectionZone == null)
    {
      _DetectionZone = GameObject.Find("DetectionPosition");
    }
    if (_centerPoint == null)
    {
      Debug.LogWarning("CenterPoint with ID 4 not found.");
    }
  }
  private void Update()
  {
    if (_centerPoint != null)
    {
      Debug.Log("_Centerpoint X = " + _centerPoint.transform.position.x);
      Debug.Log("_Centerpoint Y = " + _centerPoint.transform.position.y);
      if (_centerPoint.transform.position.x <= -2.5 || _centerPoint.transform.position.x >= 2.5 || _centerPoint.transform.position.y <= -2.5 || _centerPoint.transform.position.y >= 2.5)
      {
        Debug.LogWarning("Your face is not Center");
        _isCenterPoint = false;
        _centerPoint.GetComponent<PointAnnotation>().SetRadius(1f);
        _DetectionZone.SetActive(_isCenterPoint);
        _centerText.SetActive(true);
        _CenterNoseAlignment.SetActive(true);
      }
      else
      {
        _isCenterPoint = true;
        _centerPoint.GetComponent<PointAnnotation>().SetRadius(0f);
        _DetectionZone.SetActive(_isCenterPoint);
        _centerText.SetActive(false);
        _CenterNoseAlignment.SetActive(false);
      }
    }
  }
}

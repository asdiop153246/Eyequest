using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity;
using TMPro;
public class GetPointID : MonoBehaviour
{
  public GameObject _centerPoint;
  public bool _isCenterPoint = true;
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
        _centerPoint.gameObject.name = "CenterPoint";
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
    if (_centerPoint != null && _DetectionZone != null)
    {
      Vector3 centerPos = _centerPoint.transform.position;
      Vector3 detectionZonePos = _DetectionZone.transform.position;

      _DetectionZone.transform.position = new Vector3(centerPos.x, centerPos.y, detectionZonePos.z);
    }
  }
}

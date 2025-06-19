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
  public GameObject _CameraCheckText;


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
    // if (_CameraCheckText == null)
    // {
    //   _CameraCheckText = GameObject.Find("CameraCheckText");
    //   if (_CameraCheckText != null && _centerPoint != null)
    //   {
    //     _CameraCheckText.GetComponent<TextMeshProUGUI>().text = "Found CenterPoint";
    //   }
    // }
  }
  private void Update()
  {
    if (_centerPoint == null)
    {
      Debug.LogWarning("CenterPoint with ID 4 not found.");
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
    }
      float centerX = _centerPoint.transform.position.x;
      float centerY = _centerPoint.transform.position.y;
      
      float idealX = -2.672843f;
      float idealY = 5.905392f;
      float toleranceX = 0.5f;
      float toleranceY = 0.5f;
    if (_centerPoint != null && _DetectionZone != null)
    {
      //Debug.Log("_Centerpoint X = " + _centerPoint.transform.position.x);
      //Debug.Log("_Centerpoint Y = " + _centerPoint.transform.position.y);
      if (Mathf.Abs(centerX - idealX) > toleranceX || Mathf.Abs(centerY - idealY) > toleranceY)
      {
        //Debug.LogWarning("Your face is not Center");
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



      // Vector3 centerPos = _centerPoint.transform.position;
      // Vector3 detectionZonePos = _DetectionZone.transform.position;

      // _DetectionZone.transform.position = new Vector3(centerPos.x, centerPos.y, detectionZonePos.z);
    }
  }
}

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
            _CenterNoseAlignment = GameObject.Find("CenterNoseAlignment");

        if (_centerText == null)
        {
            _centerText = GameObject.Find("CenterfaceText");
            if (_centerText != null)
            {
                _centerText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            }
        }

        PointAnnotation[] points = GetComponentsInChildren<PointAnnotation>();
        foreach (var point in points)
        {
            if (point != null && point._id == 0)
            {
                _centerPoint = point.gameObject;
                _centerPoint.name = "CenterPoint";
                Debug.Log("CenterPoint found: " + _centerPoint.name);

                // Add TriggerDetectionHandler if not already
                if (_centerPoint.GetComponent<TriggerDetectionHandler>() == null)
                {
                    var triggerScript = _centerPoint.AddComponent<TriggerDetectionHandler>();
                    triggerScript.pointManager = this;
                }
            }
        }

        if (_DetectionZone == null)
            _DetectionZone = GameObject.Find("DetectionPosition");

        if (_centerPoint == null)
            Debug.LogWarning("CenterPoint with ID 0 not found.");
    }

  private void OnEnable()
  {
    if (_DetectionZone == null)
    _DetectionZone = GameObject.Find("DetectionPosition");
    if (_centerText != null)
      _centerText.SetActive(false);

    if (_DetectionZone != null)
      _DetectionZone.SetActive(false);

    if (_CenterNoseAlignment != null)
      _CenterNoseAlignment.SetActive(true);
            
    }

    private void OnDisable()
    {
        if (_DetectionZone != null)
            _DetectionZone.SetActive(false);

        if (_centerText != null)
            _centerText.SetActive(false);

        if (_CenterNoseAlignment != null)
            _CenterNoseAlignment.SetActive(false);
    }

  public void OnCenterEnteredBox()
  {
    _isCenterPoint = true;

    if (_DetectionZone != null)
      _DetectionZone.SetActive(true);

    if (_centerText != null)
      _centerText.SetActive(false);

        // if (_CenterNoseAlignment != null)
    //     _CenterNoseAlignment.SetActive(false);
  }
}

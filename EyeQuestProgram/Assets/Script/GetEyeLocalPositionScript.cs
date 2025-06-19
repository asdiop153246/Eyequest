using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity;
public class GetEyeLocalPositionScript : MonoBehaviour
{
  public bool _isRightIris = false;
  public GameObject _rightIris;
  public GameObject _leftIris;
  private void Update()
  {
    PointAnnotation[] points = GetComponentsInChildren<PointAnnotation>();


    foreach (var point in points)
    {
      //Debug.Log("Point ID: " + point._id);
      if (point != null && point._id == 0)
      {
          point.GetComponent<SphereCollider>().enabled = true;
      }

      if (point != null && point._id == 1 && _isRightIris == true)
      {
        _rightIris = point.gameObject;
        //Debug.Log("Right Iris Found: " + _rightIris.name);
      }
      else if (point != null && point._id == 3 && _isRightIris == false)
      {
        _leftIris = point.gameObject;
        //Debug.Log("Left Iris Found: " + _leftIris.name);
      }
      else if (_rightIris != null && _leftIris != null)
      {
        Debug.Log("Both Irises Found");
        break;
      }
    }
    if (_rightIris != null)
    {
      Vector3 localPosition = _rightIris.transform.localPosition;
      //Debug.Log("Right Iris Local Position: " + localPosition);
    }
    else if (_leftIris != null)
    {
      Vector3 localPosition = _leftIris.transform.localPosition;
      //Debug.Log("Left Iris Local Position: " + localPosition);
    }
  }

}

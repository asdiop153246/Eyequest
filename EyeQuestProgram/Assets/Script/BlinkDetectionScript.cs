using UnityEngine;
using Mediapipe.Unity;
using TMPro;
public class BlinkDetectionScript : MonoBehaviour
{
  public GameObject _Toppoint;
  public GameObject _Bottompoint;
  public GameObject _blinkText;
  private Player _player;
  public int _blinkCount;
  private float blinkTimer = 0f;
  private float holdBlinkThreshold = 1f;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    _player = FindObjectOfType<Player>();
    if (_player == null)
    {
      Debug.LogError("Player not found in the scene.");
      return;
    }
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
      if (_player.isChoosingBlink == false)
      {
        _blinkText.SetActive(false);
        return; // Exit if player is not choosing blink
      }
      Vector3 topLocalPosition = _Toppoint.transform.localPosition;
      Vector3 bottomLocalPosition = _Bottompoint.transform.localPosition;
      float distance = Vector3.Distance(topLocalPosition, bottomLocalPosition);

      bool isBlinking = distance < 13.5f;

      if (isBlinking)
      {
        blinkTimer += Time.deltaTime;
        _blinkText.SetActive(true);

        if (!wasBlinking)
        {
          _blinkCount++;
          Debug.Log("Blink Count: " + _blinkCount);
        }

        if (blinkTimer >= holdBlinkThreshold)
        {
          _blinkText.GetComponent<TextMeshProUGUI>().text = "Hold Blink";
          _player.stats.isImmune = true;
          _player.isChoosingBlink = false;
          _player.Attack(7);
        Debug.Log("Blink Detected! Player is now immune.");
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

      wasBlinking = isBlinking;
      if (_blinkCount >= 5)
      {

        _blinkCount = 0; // Reset count after detection
      }
    }
  }
  private bool wasBlinking = false;

}

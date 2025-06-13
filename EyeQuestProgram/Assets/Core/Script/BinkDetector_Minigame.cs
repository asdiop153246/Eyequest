using UnityEngine;
using Mediapipe.Unity;
using TMPro;
using UnityEngine.SceneManagement;
public class BinkDetector_Minigame : MonoBehaviour
{
    public GameObject _Toppoint;
    public GameObject _Bottompoint;
    public GameObject _Core;
    public int _blinkCount;
    private float blinkTimer = 0f;
    private float holdBlinkThreshold = 1f;

    public int _CurrentScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        _Core = GameObject.Find("GameManager");

        if (_Core == null)
        {
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
    private bool wasBlinking = false;
    public float distance;
    private void Update()
    {
        switch (_CurrentScene)
        {
            case 3:
                if (_Core.GetComponent<Minigame_1_core>()._isBlink == false)
                {
                    return;
                }
                break;
            case 4:
                if (_Core.GetComponent<Minigame_2_core>()._isBlink == false)
                {
                    return;
                }

                break;
            case 5:
                if (_Core.GetComponent<Minigame_3_core>()._isBlink == false)
                {
                    return;
                }
                break;
            case 6:
                if (_Core.GetComponent<Minigame_core_4>()._isBlink == false)
                {
                    return;
                }
                break;
        }

        if (_Toppoint != null && _Bottompoint != null)
        {
            Vector3 topLocalPosition = _Toppoint.transform.localPosition;
            Vector3 bottomLocalPosition = _Bottompoint.transform.localPosition;
            distance = Vector3.Distance(topLocalPosition, bottomLocalPosition);

            if (distance < 15f)
            {
                if (!wasBlinking)
                {
                    blinkTimer += Time.deltaTime;

                    if (blinkTimer >= 0.1f)
                    {
                        switch (_CurrentScene)
                        {
                            case 3:
                                _Core.GetComponent<Minigame_1_core>()._isBlink = false;
                                _Core.GetComponent<Minigame_1_core>()._DoneVision();
                                break;
                            case 4:
                                _Core.GetComponent<Minigame_2_core>()._isBlink = false;
                                _Core.GetComponent<Minigame_2_core>()._DoneVision();
                                break;
                            case 5:
                                _Core.GetComponent<Minigame_3_core>()._isBlink = false;
                                _Core.GetComponent<Minigame_3_core>().JudgeDistance();
                                break;
                            case 6:
                                //_Core.GetComponent<Minigame_core_4>()._isBlink = false;
                                _Core.GetComponent<Minigame_core_4>()._isHide = true;
                                //_Core.GetComponent<Minigame_core_4>()._DoneVision();
                                break;
                        }

                        wasBlinking = true;
                        blinkTimer = 0f;
                    }
                }
                
            }
            else
            {
                if (_CurrentScene == 6)
                {
                    _Core.GetComponent<Minigame_core_4>()._isHide = false;
                }
               
                wasBlinking = false;
                blinkTimer = 0f;
            }

        }
        else
        {

            if (_CurrentScene == 6)
            {
                _Core.GetComponent<Minigame_core_4>()._isHide = false;
            }

            blinkTimer = 0f;
        }

    }
}

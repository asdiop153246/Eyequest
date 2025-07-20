using UnityEngine;
using UnityEngine.UI;

public class TriggerDetectionHandler : MonoBehaviour
{
    public GetPointID pointManager;
    public float requiredHoldTime = 1.5f;
    public GameObject calibrationBoxToDisable; // ⬅️ Assign this in Inspector

    private float currentHoldTime = 0f;
    private bool isInside = false;
    private Image fillImage;

    private void Start()
    {
        // Try to find child Image to use as fill bar
        fillImage = GetComponentInChildren<Image>();
        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (isInside)
        {
            currentHoldTime += Time.deltaTime;
            Debug.Log("Timer: " + currentHoldTime);

            if (fillImage != null)
                fillImage.fillAmount = currentHoldTime / requiredHoldTime;

            if (currentHoldTime >= requiredHoldTime)
            {
                Debug.Log("Calling CompleteCalibration()");
                CompleteCalibration();
            }
        }
    }
    void OnEnable()
    {
        calibrationBoxToDisable.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CalibrationBox") && !isInside)
        {
            Debug.Log("CenterPoint entered CalibrationBox");
            isInside = true;
            currentHoldTime = 0f;
            calibrationBoxToDisable = other.gameObject;

            if (fillImage != null)
                fillImage.fillAmount = 0f;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CalibrationBox"))
        {
            Debug.Log("CenterPoint exited CalibrationBox");
            isInside = false;
            currentHoldTime = 0f;

            if (fillImage != null)
                fillImage.fillAmount = 0f;
        }
    }

    private void CompleteCalibration()
    {
        if (pointManager != null)
        {
            pointManager.OnCenterEnteredBox();
        }

        isInside = false;
        currentHoldTime = 0f;

        if (fillImage != null)
            fillImage.fillAmount = 1f;

        // ✅ Disable the calibration box
        if (calibrationBoxToDisable != null)
        {
            calibrationBoxToDisable.SetActive(false);
            Debug.Log("CalibrationBox deactivated.");
        }

        Debug.Log("Calibration Complete!");
    }
}

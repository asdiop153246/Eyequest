using UnityEngine;
using Mediapipe.Unity;
using TMPro;
public class BlinkDetectionScript : MonoBehaviour
{
    public GameObject _Toppoint;
    public GameObject _Bottompoint;
    public GameObject _blinkText;
    public Transform CenterPoint;
    public GetPointID pointID;
    public GameObject detectionPosition;
    private Player _player;
    public int _blinkCount;
    private float blinkTimer = 0f;
    private float holdBlinkThreshold = 2f;
    
    [Header("Detection Position Settings")]
    public float fixedDistanceFromCamera = 5f; // Fixed distance for stability
    public Vector3 detectionOffset = new Vector3(0, -0.5f, 0); // Offset from center point
    
    [Header("Boundary Limits")]
    public Vector2 minBounds = new Vector2(-3.5f, -2.5f); // Minimum X,Y bounds
    public Vector2 maxBounds = new Vector2(2.5f, 1.5f);   // Maximum X,Y bounds
    public bool enableBoundaries = true;
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
            else if (CenterPoint == null && point._id == 168)
            {
                CenterPoint = point.transform;
                Debug.Log("Center Point Found: " + CenterPoint.name);
                //SetupDetectionPosition();
            }
        }

    }
    private void SetupDetectionPosition()
    {
        if (CenterPoint != null)
        {
            Debug.Log("CenterPoint found with position: " + CenterPoint.position);
            if (detectionPosition != null)
            {
                Debug.Log("DetectionPosition found and will follow " + CenterPoint.name);
            }
            else
            {
                // Try to find DetectionPosition in the scene
                detectionPosition = GameObject.Find("DetectionPosition");
                if (detectionPosition != null)
                {
                    Debug.Log("DetectionPosition found and will follow " + CenterPoint.name);
                }
                else
                {
                    Debug.LogWarning("DetectionPosition GameObject not found in the scene.");
                }
            }
        }
        else
        {
            Debug.LogWarning("CenterPoint is null, cannot setup DetectionPosition.");
        }
    }


    private void Update()
    {
        if (detectionPosition == null)
        {
            detectionPosition = pointID._DetectionZone;
            if (detectionPosition != null)
            {
                Debug.Log("DetectionPosition found.");
            }
            else
            {
                Debug.LogWarning("DetectionPosition not found.");
            }
        }
        else if (CenterPoint != null)
        {
            // Debug the current CenterPoint position
            Debug.Log("CenterPoint position: " + CenterPoint.position);
            
            // SOLUTION 1: Camera-relative positioning with boundary clamping
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                // Convert to screen space first, then back to world at fixed distance
                Vector3 screenPos = mainCamera.WorldToScreenPoint(CenterPoint.position);
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, fixedDistanceFromCamera));
                worldPos += detectionOffset;
                
                // Apply boundary clamping if enabled
                if (enableBoundaries)
                {
                    worldPos.x = Mathf.Clamp(worldPos.x, minBounds.x, maxBounds.x);
                    worldPos.y = Mathf.Clamp(worldPos.y, minBounds.y, maxBounds.y);
                    Debug.Log("DetectionPosition clamped within bounds: " + worldPos);
                }
                
                detectionPosition.transform.position = worldPos;
                Debug.Log("DetectionPosition set to: " + worldPos);
            }
            else
            {
                // Fallback method with boundaries
                Vector3 followPosition = CenterPoint.position;
                followPosition.y = CenterPoint.position.y - 0.5f;
                
                // Apply boundary clamping if enabled
                if (enableBoundaries)
                {
                    followPosition.x = Mathf.Clamp(followPosition.x, minBounds.x, maxBounds.x);
                    followPosition.y = Mathf.Clamp(followPosition.y, minBounds.y, maxBounds.y);
                }
                
                detectionPosition.transform.position = followPosition;
                Debug.Log("DetectionPosition set to (fallback): " + followPosition);
            }
        }
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
            if (_player.isChoosingBlink == false && _player.isChoosingShield == false)
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
                Userdata.Instance._Haptic();
                _blinkText.SetActive(true);

                if (!wasBlinking && _player.isChoosingBlink == true)
                {
                    _blinkCount++;
                    Debug.Log("Blink Count: " + _blinkCount);
                }
                if (blinkTimer >= holdBlinkThreshold && _player.isChoosingShield == true)
                {

                    _blinkText.GetComponent<TextMeshProUGUI>().text = "Hold Blink";
                    _player.stats.isImmune = true;
                    _player.isChoosingBlink = false;
                    _player.isChoosingShield = false;
                    blinkTimer = 0f; // Reset timer after detection
                    _player.Attack(7);
                    Userdata.Instance._Haptic();
                    Debug.Log("Blink Detected! Player is now immune.");
                }
            }
            else
            {
                blinkTimer = 0f;
                _blinkText.SetActive(false);
            }

            wasBlinking = isBlinking;
            if (_blinkCount >= 1 & _player.isChoosingShield == false)
            {
                Userdata.Instance._Haptic();
                _player.Attack(6);
                _player.isChoosingBlink = false;
                _player.isChoosingShield = false;
                _blinkCount = 0; // Reset count after detection
            }
        }

    }

    private bool wasBlinking = false;

    // Helper method to visualize boundaries in Scene view
    private void OnDrawGizmosSelected()
    {
        if (enableBoundaries)
        {
            // Set gizmo color
            Gizmos.color = Color.yellow;
            
            // Calculate boundary corners
            Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2f, (minBounds.y + maxBounds.y) / 2f, fixedDistanceFromCamera);
            Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 0.1f);
            
            // Draw boundary rectangle
            Gizmos.DrawWireCube(center, size);
            
            // Draw corner markers
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(minBounds.x, minBounds.y, fixedDistanceFromCamera), 0.1f);
            Gizmos.DrawSphere(new Vector3(maxBounds.x, maxBounds.y, fixedDistanceFromCamera), 0.1f);
        }
    }

}

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
    public GameManager _gameManager;
    public int _blinkCount;
    private float blinkTimer = 0f;
    private float holdBlinkThreshold = 2f;
    
    // Non-serialized fields to avoid build conflicts
    private float fixedDistanceFromCamera = 5f; // Fixed distance for stability
    private Vector3 detectionOffset = new Vector3(0, -0.5f, 0); // Offset from center point
    private Vector2 minBounds = new Vector2(-3.5f, -2.5f); // Minimum X,Y bounds
    private Vector2 maxBounds = new Vector2(2.5f, 1.5f);   // Maximum X,Y bounds
    private bool enableBoundaries = false; // TEMPORARILY DISABLED FOR TESTING
    private bool useMobileOptimization = true;
    private float mobileScaleFactor = 0.8f; // Scale factor for mobile positioning
    
    // Manual positioning fields - private to avoid serialization conflicts
    private bool useManualPositioning = false;
    private float manualX = -2.63f; // Working position found by user
    private float manualY = -0.08f; // Adjusted down by 6 units (5.92 - 6 = -0.08)
    private float manualZ = 8.89f;  // Working position found by user
    
    // Reference position that works correctly on mobile (adjusted Y down by 6 units)
    private Vector3 workingMobilePosition = new Vector3(-2.63f, -0.08f, 8.89f);
    
    // Variables for tracking initial face position
    private Vector3 initialCenterPoint = Vector3.zero;
    private bool hasInitialPoint = false;
    
    // Method to configure boundary settings (call this in Start if needed)
    public void ConfigureBoundaries(Vector2 minBounds, Vector2 maxBounds, float distance = 5f, bool enabled = true)
    {
        this.minBounds = minBounds;
        this.maxBounds = maxBounds;
        this.fixedDistanceFromCamera = distance;
        this.enableBoundaries = enabled;
        Debug.Log($"Boundaries configured: Min{minBounds}, Max{maxBounds}, Distance{distance}, Enabled{enabled}");
    }
    void OnDisable()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager != null)
        {
            _gameManager.CancelSkillandDisableUI();
        }
    }
    // Mobile-specific positioning method
    private Vector3 GetMobileOptimizedPosition(Vector3 centerPointPos)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return centerPointPos;
        
        // Convert to viewport coordinates (0-1 range)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(centerPointPos);
        
        // Ensure position is within screen bounds
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.1f, 0.9f); // Keep within 10-90% of screen width
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.1f, 0.9f); // Keep within 10-90% of screen height
        
        // Apply mobile scale factor
        Vector3 center = new Vector3(0.5f, 0.5f, viewportPos.z);
        viewportPos = Vector3.Lerp(center, viewportPos, mobileScaleFactor);
        
        // Convert back to world coordinates at fixed distance
        viewportPos.z = fixedDistanceFromCamera;
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPos);
        
        Debug.Log($"Mobile optimized position: Viewport{viewportPos} -> World{worldPos}");
        return worldPos;
    }
    
    // Real mobile device positioning - simpler and more reliable
    private Vector3 GetRealMobilePosition(Vector3 centerPointPos)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) 
        {
            Debug.LogWarning("No main camera found for mobile positioning");
            return centerPointPos;
        }
        
        // Method 1: Direct relative positioning with heavy damping
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 targetPos = mainCamera.transform.position + cameraForward * 2f; // Fixed 2 units in front
        
        // Add small offset based on center point (but heavily dampened)
        Vector3 localOffset = mainCamera.transform.InverseTransformPoint(centerPointPos);
        localOffset.x *= 0.3f; // Reduce X movement
        localOffset.y *= 0.3f; // Reduce Y movement
        localOffset.z = 0f;    // Ignore Z movement
        
        targetPos += mainCamera.transform.TransformDirection(localOffset);
        
        // Add detection offset
        targetPos += detectionOffset;
        
        Debug.Log($"Real mobile position: CenterPoint{centerPointPos} -> Offset{localOffset} -> Final{targetPos}");
        return targetPos;
    }
    
    // New method: Use working position with relative face movement (immediate following)
    private Vector3 GetWorkingMobilePosition(Vector3 centerPointPos)
    {
        // Start with the known working position as base
        Vector3 targetPos = workingMobilePosition;
        
        // Debug current state
        Debug.Log($"GetWorkingMobilePosition called - centerPointPos: {centerPointPos}");
        
        // Apply face position with much more conservative scaling to keep it on screen
        // Reduce the scaling significantly to prevent going off-screen
        targetPos.x = workingMobilePosition.x + (centerPointPos.x * 0.1f); // Much smaller X scaling
        targetPos.y = workingMobilePosition.y + (centerPointPos.y * 0.1f); // Much smaller Y scaling
        
        // Clamp to ensure it stays near working position
        targetPos.x = Mathf.Clamp(targetPos.x, workingMobilePosition.x - 1f, workingMobilePosition.x + 1f);
        targetPos.y = Mathf.Clamp(targetPos.y, workingMobilePosition.y - 1f, workingMobilePosition.y + 1f);
        // Keep Z position fixed at working distance
        
        Debug.Log($"Working mobile position: Base{workingMobilePosition} + Conservative Face{centerPointPos * 0.1f} -> Clamped{targetPos}");
        return targetPos;
    }
    
    // Public method to adjust mobile settings during runtime
    public void AdjustMobileSettings(float scaleFactor, Vector2 newMinBounds, Vector2 newMaxBounds)
    {
        mobileScaleFactor = scaleFactor;
        minBounds = newMinBounds;
        maxBounds = newMaxBounds;
        Debug.Log($"Mobile settings adjusted: Scale{scaleFactor}, Bounds[{newMinBounds} to {newMaxBounds}]");
    }
    
    // Method to reset the face position reference (useful for recalibration)
    public void ResetFacePositionReference()
    {
        hasInitialPoint = false;
        initialCenterPoint = Vector3.zero;
        Debug.Log("Face position reference reset - will recalibrate on next frame");
    }
    
    // Method to update the working position based on current good position
    public void SetWorkingPosition(Vector3 newWorkingPosition)
    {
        workingMobilePosition = newWorkingPosition;
        Debug.Log($"Working position updated to: {workingMobilePosition}");
        ResetFacePositionReference(); // Reset reference to start fresh
    }
    
    // Debug method to test if detection position can move at all
    public void TestDetectionPositionMovement()
    {
        if (detectionPosition != null)
        {
            Vector3 testPos = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(3f, 10f));
            detectionPosition.transform.position = testPos;
            Debug.Log($"TEST: Forced DetectionPosition to random position: {testPos}");
            
            // Also check if the object is active and visible
            Debug.Log($"DetectionPosition active: {detectionPosition.activeInHierarchy}");
            Debug.Log($"DetectionPosition name: {detectionPosition.name}");
            
            // List components without LINQ
            Component[] components = detectionPosition.GetComponents<Component>();
            string componentNames = "";
            for (int i = 0; i < components.Length; i++)
            {
                componentNames += components[i].GetType().Name;
                if (i < components.Length - 1) componentNames += ", ";
            }
            Debug.Log($"DetectionPosition components: {componentNames}");
        }
        else
        {
            Debug.LogWarning("TEST: DetectionPosition is null, cannot test movement");
        }
    }
    
    // Method to test the exact working position you found
    public void SetToWorkingPosition()
    {
        if (detectionPosition != null)
        {
            Vector3 exactWorkingPos = new Vector3(-2.63f, 5.92f, 8.89f);
            detectionPosition.transform.position = exactWorkingPos;
            Debug.Log($"Set DetectionPosition to exact working coordinates: {exactWorkingPos}");
        }
    }
    
    // Method to test conservative positioning around center
    public void TestConservativePositioning()
    {
        if (detectionPosition != null && Camera.main != null)
        {
            // Try positioning at screen center at different depths
            Camera cam = Camera.main;
            Vector3 screenCenter = new Vector3(UnityEngine.Screen.width / 2f, UnityEngine.Screen.height / 2f, 5f);
            Vector3 worldCenter = cam.ScreenToWorldPoint(screenCenter);
            detectionPosition.transform.position = worldCenter;
            Debug.Log($"Set DetectionPosition to screen center in world: {worldCenter}");
        }
    }
    
    // Method to enable/disable manual positioning for testing
    public void SetManualPositioning(bool enabled)
    {
        useManualPositioning = enabled;
        Debug.Log($"Manual positioning {(enabled ? "ENABLED" : "DISABLED")}");
    }
    
    // Method to force center the detection position (useful for testing)
    public void CenterDetectionPosition()
    {
        if (detectionPosition != null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                Vector3 centerScreen = new Vector3(UnityEngine.Screen.width / 2f, UnityEngine.Screen.height / 2f, fixedDistanceFromCamera);
                Vector3 centerWorld = mainCamera.ScreenToWorldPoint(centerScreen);
                detectionPosition.transform.position = centerWorld;
                Debug.Log("DetectionPosition forced to center: " + centerWorld);
            }
        }
    }
    
    // Mobile debugging - creates on-screen debug info
    private GameObject debugUI;
    private GameObject sliderUI;
    
    public void CreateMobileDebugUI()
    {
        if (debugUI == null && Application.isMobilePlatform)
        {
            // Create a simple UI text for mobile debugging
            debugUI = new GameObject("MobileDebugUI");
            Canvas canvas = debugUI.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;
            
            GameObject textObj = new GameObject("DebugText");
            textObj.transform.SetParent(debugUI.transform);
            
            TMPro.TextMeshProUGUI debugText = textObj.AddComponent<TMPro.TextMeshProUGUI>();
            debugText.text = "Mobile Debug Started";
            debugText.fontSize = 24;
            debugText.color = Color.white;
            
            RectTransform rect = textObj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(10, -200);
            rect.offsetMax = new Vector2(-10, -10);
            
            Debug.Log("Mobile debug UI created");
        }
    }
    
    // Create manual positioning sliders
    public void CreatePositionSliders()
    {
        if (sliderUI != null) return; // Already created
        
        // Create UI canvas for sliders
        sliderUI = new GameObject("PositionSliderUI");
        Canvas canvas = sliderUI.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1001; // Above debug UI
        
        // Add CanvasScaler for responsive UI
        UnityEngine.UI.CanvasScaler scaler = sliderUI.AddComponent<UnityEngine.UI.CanvasScaler>();
        scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Add GraphicRaycaster for interaction
        sliderUI.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        
        // Create panel background
        GameObject panel = new GameObject("SliderPanel");
        panel.transform.SetParent(sliderUI.transform);
        
        UnityEngine.UI.Image panelImage = panel.AddComponent<UnityEngine.UI.Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f); // Semi-transparent black
        
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 0);
        panelRect.anchorMax = new Vector2(1, 0);
        panelRect.offsetMin = new Vector2(10, 10);
        panelRect.offsetMax = new Vector2(-10, 250);
        
        // Create sliders
        CreateSlider(panel, "X Position", -10f, 10f, manualX, (value) => { manualX = value; }, 0);
        CreateSlider(panel, "Y Position", -10f, 10f, manualY, (value) => { manualY = value; }, 1);
        CreateSlider(panel, "Z Position", 1f, 20f, manualZ, (value) => { manualZ = value; }, 2);
        
        // Create toggle for manual positioning
        CreateToggle(panel, "Manual Positioning", useManualPositioning, (value) => { useManualPositioning = value; });
        
        Debug.Log("Position sliders created");
    }
    
    // Helper method to create individual sliders
    private void CreateSlider(GameObject parent, string label, float minValue, float maxValue, float currentValue, System.Action<float> onValueChanged, int index)
    {
        GameObject sliderObj = new GameObject($"Slider_{label}");
        sliderObj.transform.SetParent(parent.transform);
        
        // Create label
        GameObject labelObj = new GameObject("Label");
        labelObj.transform.SetParent(sliderObj.transform);
        
        TMPro.TextMeshProUGUI labelText = labelObj.AddComponent<TMPro.TextMeshProUGUI>();
        labelText.text = label;
        labelText.fontSize = 18;
        labelText.color = Color.white;
        
        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        labelRect.anchorMin = new Vector2(0, 0);
        labelRect.anchorMax = new Vector2(0.3f, 1);
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;
        
        // Create slider background
        GameObject sliderBg = new GameObject("Background");
        sliderBg.transform.SetParent(sliderObj.transform);
        
        UnityEngine.UI.Image bgImage = sliderBg.AddComponent<UnityEngine.UI.Image>();
        bgImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        
        RectTransform bgRect = sliderBg.GetComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0.35f, 0.2f);
        bgRect.anchorMax = new Vector2(0.9f, 0.8f);
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
        
        // Create slider handle
        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(sliderBg.transform);
        
        UnityEngine.UI.Image handleImage = handle.AddComponent<UnityEngine.UI.Image>();
        handleImage.color = Color.white;
        
        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleRect.sizeDelta = new Vector2(20, 0);
        
        // Create slider component
        UnityEngine.UI.Slider slider = sliderBg.AddComponent<UnityEngine.UI.Slider>();
        slider.targetGraphic = handleImage;
        slider.handleRect = handleRect;
        slider.direction = UnityEngine.UI.Slider.Direction.LeftToRight;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = currentValue;
        slider.onValueChanged.AddListener(onValueChanged.Invoke);
        
        // Create value display
        GameObject valueObj = new GameObject("Value");
        valueObj.transform.SetParent(sliderObj.transform);
        
        TMPro.TextMeshProUGUI valueText = valueObj.AddComponent<TMPro.TextMeshProUGUI>();
        valueText.text = currentValue.ToString("F2");
        valueText.fontSize = 16;
        valueText.color = Color.yellow;
        
        RectTransform valueRect = valueObj.GetComponent<RectTransform>();
        valueRect.anchorMin = new Vector2(0.9f, 0);
        valueRect.anchorMax = new Vector2(1, 1);
        valueRect.offsetMin = Vector2.zero;
        valueRect.offsetMax = Vector2.zero;
        
        // Update value text when slider changes
        slider.onValueChanged.AddListener((value) => {
            valueText.text = value.ToString("F2");
        });
        
        // Position the slider
        RectTransform sliderRect = sliderObj.GetComponent<RectTransform>();
        sliderRect.anchorMin = new Vector2(0, 1 - (index + 1) * 0.25f);
        sliderRect.anchorMax = new Vector2(1, 1 - index * 0.25f);
        sliderRect.offsetMin = new Vector2(10, 5);
        sliderRect.offsetMax = new Vector2(-10, -5);
    }
    
    // Helper method to create toggle
    private void CreateToggle(GameObject parent, string label, bool currentValue, System.Action<bool> onValueChanged)
    {
        GameObject toggleObj = new GameObject("Toggle_Manual");
        toggleObj.transform.SetParent(parent.transform);
        
        // Create toggle background
        UnityEngine.UI.Image toggleBg = toggleObj.AddComponent<UnityEngine.UI.Image>();
        toggleBg.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        
        // Create checkmark
        GameObject checkmark = new GameObject("Checkmark");
        checkmark.transform.SetParent(toggleObj.transform);
        
        UnityEngine.UI.Image checkImage = checkmark.AddComponent<UnityEngine.UI.Image>();
        checkImage.color = Color.green;
        checkImage.enabled = currentValue;
        
        RectTransform checkRect = checkmark.GetComponent<RectTransform>();
        checkRect.anchorMin = new Vector2(0.2f, 0.2f);
        checkRect.anchorMax = new Vector2(0.8f, 0.8f);
        checkRect.offsetMin = Vector2.zero;
        checkRect.offsetMax = Vector2.zero;
        
        // Create toggle component
        UnityEngine.UI.Toggle toggle = toggleObj.AddComponent<UnityEngine.UI.Toggle>();
        toggle.targetGraphic = toggleBg;
        toggle.graphic = checkImage;
        toggle.isOn = currentValue;
        toggle.onValueChanged.AddListener(onValueChanged.Invoke);
        
        // Create label
        GameObject labelObj = new GameObject("Label");
        labelObj.transform.SetParent(parent.transform);
        
        TMPro.TextMeshProUGUI labelText = labelObj.AddComponent<TMPro.TextMeshProUGUI>();
        labelText.text = label;
        labelText.fontSize = 18;
        labelText.color = Color.white;
        
        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        labelRect.anchorMin = new Vector2(0.15f, 0.75f);
        labelRect.anchorMax = new Vector2(0.8f, 0.95f);
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;
        
        // Position the toggle
        RectTransform toggleRect = toggleObj.GetComponent<RectTransform>();
        toggleRect.anchorMin = new Vector2(0.05f, 0.78f);
        toggleRect.anchorMax = new Vector2(0.12f, 0.92f);
        toggleRect.offsetMin = Vector2.zero;
        toggleRect.offsetMax = Vector2.zero;
    }
    
    // Update mobile debug UI with current info
    private void UpdateMobileDebugUI()
    {
        if (debugUI != null && CenterPoint != null)
        {
            TMPro.TextMeshProUGUI debugText = debugUI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (debugText != null)
            {
                debugText.text = $"Platform: {(Application.isMobilePlatform ? "Mobile" : "Desktop")}\n" +
                                $"IsEditor: {Application.isEditor}\n" +
                                $"CenterPoint: {CenterPoint.position}\n" +
                                $"DetectionPos: {(detectionPosition ? detectionPosition.transform.position.ToString() : "NULL")}\n" +
                                $"Screen: {UnityEngine.Screen.width}x{UnityEngine.Screen.height}";
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // // Create mobile debug UI if on mobile device
        // if (Application.isMobilePlatform && !Application.isEditor)
        // {
        //     CreateMobileDebugUI();
        // }
        
        // // Always create position sliders for testing
        // CreatePositionSliders();
        
        // Detect mobile platform and adjust settings
        if (Application.isMobilePlatform)
        {
            useMobileOptimization = true;
            mobileScaleFactor = 0.6f; // Smaller scale for mobile
            // BOUNDARIES TEMPORARILY DISABLED FOR TESTING
            ConfigureBoundaries(new Vector2(-2f, -1.5f), new Vector2(2f, 1.5f), 3f, false);
            Debug.Log("Mobile platform detected - using mobile optimizations (boundaries disabled)");
        }
        else
        {
            // BOUNDARIES TEMPORARILY DISABLED FOR TESTING
            ConfigureBoundaries(new Vector2(-3.5f, -2.5f), new Vector2(2.5f, 1.5f), 5f, false);
            Debug.Log("Desktop platform detected - using standard settings (boundaries disabled)");
        }
        
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
        // Update mobile debug UI
        if (Application.isMobilePlatform && !Application.isEditor)
        {
            UpdateMobileDebugUI();
        }
        
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
            Vector3 worldPos;
            
            // Debug CenterPoint info
            Debug.Log($"CenterPoint is not null: {CenterPoint.position}");
            
            // Use simple direct positioning - just follow CenterPoint with offset
            worldPos = CenterPoint.position + detectionOffset;
            Debug.Log($"Using SIMPLE DIRECT positioning: CenterPoint{CenterPoint.position} + Offset{detectionOffset} = {worldPos}");
            
            // Apply boundary clamping if enabled
            if (enableBoundaries)
            {
                worldPos.x = Mathf.Clamp(worldPos.x, minBounds.x, maxBounds.x);
                worldPos.y = Mathf.Clamp(worldPos.y, minBounds.y, maxBounds.y);
                Debug.Log("DetectionPosition clamped within bounds: " + worldPos);
            }
            
            detectionPosition.transform.position = worldPos;
            Debug.Log($"FINAL DetectionPosition set to: {worldPos}");
            Debug.Log($"DetectionPosition actual position after assignment: {detectionPosition.transform.position}");
        }
        else
        {
            Debug.Log("CenterPoint is NULL - no positioning update");
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
        Debug.Log("Current Position of DetectionPosition = " + detectionPosition.transform.position);
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

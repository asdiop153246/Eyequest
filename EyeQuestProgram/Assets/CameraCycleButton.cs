using UnityEngine;
using UnityEngine.UI;
using Mediapipe.Unity;
using System.Collections;

namespace Mediapipe.Unity.Sample.UI
{
    public class CameraCycleButton : MonoBehaviour
    {
        private int _currentIndex = 0;
        public string[] _sourceNames;
        public ImageSourceConfig _imageSourceConfig;
        public ImageSource _imageSource;

        public GameObject _ConfigPanel;
        [SerializeField] public BaseRunner _baseRunner;

        void Start()
        {
            //_imageSource = ImageSourceProvider.ImageSource;
            Debug.Log($"ImageSource = {_imageSource.sourceName}");
            if (_imageSource != null && _imageSource.sourceCandidateNames != null)
            {
                _sourceNames = _imageSource.sourceCandidateNames;
                Debug.Log($"Initialized CameraCycleButton with {_sourceNames.Length} camera(s).");
            }
            else
            {
                Debug.LogWarning("ImageSource not initialized or has no cameras.");
            }
        }

        void OnEnable()
        {
            Debug.Log("CameraCycleButton enabled");

            _imageSource = ImageSourceProvider.ImageSource;

            if (_imageSource == null || _imageSource.sourceCandidateNames == null)
            {
                Debug.LogWarning("ImageSource not initialized or source list missing.");
                return;
            }

            _sourceNames = _imageSource.sourceCandidateNames;

            if (_imageSource.sourceName != "Camera 1")
            {
                Debug.Log("Current source is not Camera 1. Searching...");
                StartCoroutine(SwitchToCameraByName("Camera 1"));
            }
            else
            {
                Debug.Log("Camera 1 is already selected.");
            }
        }
        private IEnumerator SwitchToCameraByName(string targetName)
        {
            _baseRunner?.Pause();

            yield return null; // wait 1 frame to ensure pause settles

            for (int i = 0; i < _sourceNames.Length; i++)
            {
                Debug.Log($"Found Camera {_sourceNames[i]}");
                if (_sourceNames[i] == targetName)
                {
                    _imageSource.SelectSource(i);
                    _currentIndex = i;
                    // _imageSourceConfig.InitializeSourceFromOutside(1);
                    Debug.Log($"Switched to {targetName} at index {i}");

                    break;
                }
            }


            // Wait briefly for WebCamTexture to switch
            yield return new WaitForSeconds(0.5f); // Adjust if needed

            _baseRunner?.Resume();
            //_baseRunner?.Play();
            Debug.Log("Resumed BaseRunner after camera switch.");
        }

        // public void CycleCamera()
        // {
        //     if (_imageSource == null || _sourceNames == null || _sourceNames.Length == 0)
        //     {
        //         Debug.LogWarning("No camera sources to cycle.");
        //         return;
        //     }

        //     _currentIndex = (_currentIndex + 1) % _sourceNames.Length;
        //     _imageSource.SelectSource(_currentIndex);
        //     Debug.Log($"Switched to camera index {_currentIndex}: {_sourceNames[_currentIndex]}");
        // }

        // public void OpenConfigPanel()
        // {
        //     if (_ConfigPanel != null)
        //     {
        //         bool isOpen = _ConfigPanel.activeSelf;
        //         _ConfigPanel.SetActive(!isOpen);

        //         if (!isOpen)
        //         {
        //             _baseRunner?.Pause();
        //             Debug.Log("Opened Camera Configuration Panel and paused BaseRunner.");
        //         }
        //         else
        //         {
        //             _baseRunner?.Resume();
        //             _baseRunner?.Play();
        //             Debug.Log("Closed Camera Configuration Panel and resumed BaseRunner.");
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Camera Configuration Panel not assigned.");
        //     }
        // }
    }
}

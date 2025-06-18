using UnityEngine;
using Mediapipe.Unity;

namespace Mediapipe.Unity.Sample.UI
{
    public class CameraAutoSelector : MonoBehaviour
    {
        void OnEnable()
        {
            var imageSource = ImageSourceProvider.ImageSource;

            if (imageSource != null && imageSource.sourceCandidateNames != null)
            {
                var sourceNames = imageSource.sourceCandidateNames;
                Debug.Log($"Found {sourceNames.Length} camera(s):");

                for (int i = 0; i < sourceNames.Length; i++)
                {
                    Debug.Log($"Camera Index {i}: {sourceNames[i]}");

                    // Try to auto-detect front-facing camera
                    var lowerName = sourceNames[i].ToLower();
                    if (lowerName.Contains("front") || lowerName.Contains("user"))
                    {
                        imageSource.SelectSource(i);
                        Debug.Log($"Auto-switched to front camera at index {i}: {sourceNames[i]}");
                        return;
                    }
                }

                Debug.LogWarning("No front camera detected by name. Using default camera (index 1).");
                imageSource.SelectSource(2);
            }
            else
            {
                Debug.LogWarning("ImageSource not initialized or has no source candidates.");
            }
        }
    }
}

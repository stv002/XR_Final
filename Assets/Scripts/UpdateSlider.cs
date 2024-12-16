using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using Unity.XR;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates a slider and associated text to control and display camera offset height in XR.
/// </summary>
public class UpdateSlider : MonoBehaviour
{
    public Slider slider;                // Slider UI element for adjusting camera offset
    public TextMeshProUGUI text;         // Text element to display current offset value
    public XROrigin xrOrigin;            // XR origin from which camera offset is controlled

    private float originalOffset;        // Original camera offset value at start

    void Start()
    {
        float val = xrOrigin.CameraYOffset; // Get initial camera offset value
        originalOffset = val;               // Store the original offset
        MoveOffsetHeight(0);                // Ensure camera offset is correctly set

        slider.value = val;                 // Set slider value to initial camera offset
        text.text = val.ToString("F2");     // Display initial offset value in text
    }

    /// <summary>
    /// Updates the displayed text value when the slider value changes.
    /// </summary>
    public void UpdateText()
    {
        text.text = slider.value.ToString("F2"); // Update text to match slider value

        // Update camera offset and adjust position accordingly
        MoveOffsetHeight(slider.value - xrOrigin.CameraYOffset);
    }

    /// <summary>
    /// Restores the camera offset to its original value and updates UI accordingly.
    /// </summary>
    public void RestoreOffset()
    {
        slider.value = originalOffset;          // Reset slider to original offset value
        text.text = originalOffset.ToString("F2"); // Update text to original offset value
        xrOrigin.CameraYOffset = originalOffset; // Reset camera offset to original value
    }

    /// <summary>
    /// Moves the camera floor offset based on the delta provided.
    /// </summary>
    /// <param name="delta">Change in camera offset height.</param>
    private void MoveOffsetHeight(float delta)
    {
        var offsetTransform = xrOrigin.CameraFloorOffsetObject.transform; // Get camera floor offset object
        var desiredPosition = offsetTransform.localPosition;             // Get current local position
        desiredPosition.y = delta;                                        // Update y position based on delta
        offsetTransform.localPosition = desiredPosition;                  // Apply new position
    }
}

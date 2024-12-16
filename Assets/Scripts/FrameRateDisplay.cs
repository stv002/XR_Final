using TMPro;
using UnityEngine;

public class FrameRateDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float timeSince = 0f;
    private int frameCount = 0;

    void Update()
    {
        // Increment frame count and elapsed time
        frameCount++;
        timeSince += Time.unscaledDeltaTime;

        // Check if it's time to update
        if (timeSince >= 0.5)
        {
            float frameRate = frameCount / timeSince;

            text.text = $"FPS: {frameRate:F1}"; // One decimal place

            frameCount = 0;
            timeSince = 0f;
        }
    }
}
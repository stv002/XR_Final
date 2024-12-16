using MagicLightmapSwitcher;
using UnityEngine;

/// <summary>
/// Manages lighting scenarios by switching between different lightmaps.
/// </summary>
public class LightingManager : MonoBehaviour
{
    public StoredLightingScenario lightingScenario; // Object that holds any stored lightmaps
    public Camera forceRenderCamera; // Camera positioned to see the entire scene

    private RuntimeAPI runtimeAPI; // API for switching lightmaps

    void Start()
    {
        runtimeAPI = new RuntimeAPI(); // Initialize the runtime API

        TurnOffLights(); // Start the game with lights off
    }

    /// <summary>
    /// Turns on all lights by switching to a lightmap with all lights enabled.
    /// </summary>
    public void TurnOnLights()
    {
        forceRenderCamera.Render(); // Render to ensure objects out of view are loaded correctly
        runtimeAPI.SwitchLightmap(0, lightingScenario); // Switch to lightmap with all lights enabled
    }

    /// <summary>
    /// Turns off unnecessary lights by switching to a lightmap with minimal lighting.
    /// </summary>
    public void TurnOffLights()
    {
        forceRenderCamera.Render(); // Render to ensure objects out of view are loaded correctly
        runtimeAPI.SwitchLightmap(1, lightingScenario); // Switch to lightmap with minimal lighting
    }
}

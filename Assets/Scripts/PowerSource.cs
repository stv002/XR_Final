using UnityEngine;

/// <summary>
/// Represents a basic power source that can be turned on or off.
/// Implements the IPowerProvider interface.
/// </summary>
public class PowerSource : MonoBehaviour, IPowerProvider
{
    public bool isOn = true; // Indicates if the power source is currently on

    /// <summary>
    /// Checks if the power source is currently providing power.
    /// </summary>
    /// <returns>True if the power source is on, false otherwise.</returns>
    public bool IsPowered()
    {
        return isOn;
    }
}

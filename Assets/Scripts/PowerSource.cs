using UnityEngine;

public class PowerSource : MonoBehaviour, IPowerProvider
{
    public bool isOn = true;

    public bool IsPowered()
    {
        return isOn;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PoweredComponent : MonoBehaviour, IPowerProvider
{
    public XRSocketInteractor socketA;
    public XRSocketInteractor socketB;

    [Header("Events")]
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    private bool isPowered;

    private void OnEnable()
    {
        if (socketA != null)
        {
            socketA.selectEntered.AddListener(OnSocketChanged);
            socketA.selectExited.AddListener(OnSocketChanged);
        }

        if (socketB != null)
        {
            socketB.selectEntered.AddListener(OnSocketChanged);
            socketB.selectExited.AddListener(OnSocketChanged);
        }
    }

    private void OnDisable()
    {
        if (socketA != null)
        {
            socketA.selectEntered.RemoveListener(OnSocketChanged);
            socketA.selectExited.RemoveListener(OnSocketChanged);
        }

        if (socketB != null)
        {
            socketB.selectEntered.RemoveListener(OnSocketChanged);
            socketB.selectExited.RemoveListener(OnSocketChanged);
        }
    }

    private void OnSocketChanged(SelectEnterEventArgs args)
    {
        CheckPower();
    }

    private void OnSocketChanged(SelectExitEventArgs args)
    {
        CheckPower();
    }

    public void CheckPower()
    {
        // Both sockets must be filled
        if (!IsSocketOccupied(socketA) || !IsSocketOccupied(socketB))
        {
            SetPowered(false);
            return;
        }

        // Check what is plugged into each socket
        IPowerProvider providerA = GetPowerProviderFromSocket(socketA);
        IPowerProvider providerB = GetPowerProviderFromSocket(socketB);

        // If at least one side can trace back to a power source and the other side makes a complete path, consider it powered.
        bool aPowered = providerA != null && providerA.IsPowered();
        bool bPowered = providerB != null && providerB.IsPowered();

        SetPowered(aPowered && bPowered);
    }

    private IPowerProvider GetPowerProviderFromSocket(XRSocketInteractor socket)
    {
        var interactable = socket.selectTarget;
        if (interactable == null)
            return null;

        // Look for IPowerProvider on the object or its parents.
        return interactable.transform.GetComponentInParent<IPowerProvider>();
    }

    private bool IsSocketOccupied(XRSocketInteractor socket)
    {
        return socket.selectTarget != null;
    }

    private void SetPowered(bool state)
    {
        if (isPowered == state) return;

        isPowered = state;
        if (isPowered)
        {
            OnPoweredOn();
        }
        else
        {
            OnPoweredOff();
        }
    }

    public bool IsPowered()
    {
        return isPowered;
    }

    private void OnPoweredOn()
    {
        Debug.Log("Component powered on!");
        onActivate.Invoke();
    }

    private void OnPoweredOff()
    {
        Debug.Log("Component powered off!");
        onDeactivate.Invoke();
    }
}

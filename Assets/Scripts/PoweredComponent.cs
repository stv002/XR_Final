using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manages the power state of a component based on connections to XRSocketInteractors.
/// Implements IPowerProvider to provide power status.
/// </summary>
public class PoweredComponent : MonoBehaviour, IPowerProvider
{
    [Tooltip("First socket interactor for connection.")]
    public XRSocketInteractor socketA;

    [Tooltip("Second socket interactor for connection.")]
    public XRSocketInteractor socketB;

    [Tooltip("If true, component requires only one connection to be considered powered.")]
    public bool requireSingleConnection;

    [Header("Events")]
    public UnityEvent onActivate;   // Event invoked when component is powered on
    public UnityEvent onDeactivate; // Event invoked when component is powered off

    private bool isPowered; // Current power state of the component

    /// <summary>
    /// Subscribes to socket events to monitor connection changes.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to socket events when the script is enabled
        if (socketA != null)
        {
            socketA.selectEntered.AddListener(OnSocketEntered);
            socketA.selectExited.AddListener(OnSocketExited);
        }

        if (socketB != null)
        {
            socketB.selectEntered.AddListener(OnSocketEntered);
            socketB.selectExited.AddListener(OnSocketExited);
        }
    }

    /// <summary>
    /// Unsubscribes from socket events to avoid memory leaks.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from socket events when the script is disabled
        if (socketA != null)
        {
            socketA.selectEntered.RemoveListener(OnSocketEntered);
            socketA.selectExited.RemoveListener(OnSocketExited);
        }

        if (socketB != null)
        {
            socketB.selectEntered.RemoveListener(OnSocketEntered);
            socketB.selectExited.RemoveListener(OnSocketExited);
        }
    }

    /// <summary>
    /// Event handler for socket enter and exit events.
    /// </summary>
    private void OnSocketEntered(SelectEnterEventArgs args)
    {
        CheckPower();
    }

    /// <summary>
    /// Event handler for socket enter and exit events.
    /// </summary>
    private void OnSocketExited(SelectExitEventArgs args)
    {
        CheckPower();
    }

    /// <summary>
    /// Checks the power state based on socket connections and updates the component's state.
    /// </summary>
    public void CheckPower()
    {
        // Both sockets must be filled unless requireSingleConnection is true
        if (!requireSingleConnection && (!IsSocketOccupied(socketA) || !IsSocketOccupied(socketB)))
        {
            SetPowered(false); // Not powered if both sockets are not occupied
            return;
        }

        // Check what is plugged into each socket
        IPowerProvider providerA = GetPowerProviderFromSocket(socketA);
        IPowerProvider providerB = GetPowerProviderFromSocket(socketB);

        // Determine if each side is powered
        bool aPowered = providerA != null && providerA.IsPowered();
        bool bPowered = providerB != null && providerB.IsPowered();

        // Determine overall power state based on connection requirements
        SetPowered((requireSingleConnection && (aPowered || bPowered)) || (aPowered && bPowered));
    }

    /// <summary>
    /// Retrieves the power provider from the given socket interactor.
    /// </summary>
    private IPowerProvider GetPowerProviderFromSocket(XRSocketInteractor socket)
    {
        if (socket == null)
            return null;

        var interactable = socket.selectTarget;
        if (interactable == null)
            return null;

        // Look for IPowerProvider on the object or its parent.
        return interactable.transform.GetComponentInParent<IPowerProvider>();
    }

    /// <summary>
    /// Checks if the given socket interactor is occupied by a target.
    /// </summary>
    private bool IsSocketOccupied(XRSocketInteractor socket)
    {
        if (socket == null)
            return false;

        return socket.selectTarget != null;
    }

    /// <summary>
    /// Sets the powered state of the component and invokes corresponding events.
    /// </summary>
    private void SetPowered(bool state)
    {
        if (isPowered == state) return;

        isPowered = state;
        if (isPowered)
        {
            OnPoweredOn(); // Invoke activation event if powered on
        }
        else
        {
            OnPoweredOff(); // Invoke deactivation event if powered off
        }
    }

    /// <summary>
    /// Returns the current power state of the component.
    /// </summary>
    public bool IsPowered()
    {
        return isPowered;
    }

    /// <summary>
    /// Invoked when the component is powered on. Triggers the onActivate event.
    /// </summary>
    private void OnPoweredOn()
    {
        Debug.Log("Component powered on!");
        onActivate.Invoke();
    }

    /// <summary>
    /// Invoked when the component is powered off. Triggers the onDeactivate event.
    /// </summary>
    private void OnPoweredOff()
    {
        Debug.Log("Component powered off!");
        onDeactivate.Invoke();
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents one end of a wire that can connect to an XRSocketInteractor.
/// Implements IPowerProvider to determine if it provides power.
/// </summary>
public class WireEnd : MonoBehaviour, IPowerProvider
{
    [Tooltip("Reference to the other end of the wire.")]
    public WireEnd otherEnd;

    [Tooltip("Flag indicating if this end is powered by default.")]
    public bool isPowered;

    private IPowerProvider connectedProvider; // Holds the power provider connected directly to this end
    private XRSocketInteractor currentSocket; // Current socket this end is connected to

    private void OnEnable()
    {
        // Listen for grab events on this interactable object
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        // Clean up event listeners when disabled to prevent memory leaks
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    /// <summary>
    /// Event handler for when this object is grabbed and enters a socket.
    /// </summary>
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor socket)
        {
            currentSocket = socket;
            // Check if the socket or its parent object has an IPowerProvider interface
            connectedProvider = socket.GetComponentInParent<IPowerProvider>();

            // Notify connected objects to update power status if needed
            NotifyConnectionChanged();
        }
    }

    /// <summary>
    /// Event handler for when this object is released from a socket.
    /// </summary>
    private void OnSelectExited(SelectExitEventArgs args)
    {
        currentSocket = null;
        connectedProvider = null;

        // Notify that connection changed
        NotifyConnectionChanged();
    }

    /// <summary>
    /// Returns whether this end is powered, based on default, connected provider, or other end's power.
    /// </summary>
    public bool IsPowered()
    {
        // If this end is powered by default
        if (isPowered)
        {
            return true;
        }

        // If this end is connected to a power provider directly:
        if (connectedProvider != null && connectedProvider.IsPowered())
        {
            return true;
        }

        // Otherwise, check if the other end is powered (covers power coming from the other side)
        if (otherEnd != null && (otherEnd.PoweredByDefault() || (otherEnd.connectedProvider != null && otherEnd.connectedProvider.IsPowered())))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Notifies connected components that the connection status has changed.
    /// </summary>
    private void NotifyConnectionChanged()
    {
        // Notify the PoweredComponent on this side of the connection
        var component = GetComponentInParent<PoweredComponent>();
        if (component != null)
        {
            component.CheckPower();
        }

        // Notify the other end's PoweredComponent as well, as changes here affect it
        if (otherEnd != null)
        {
            var otherComponent = otherEnd.GetComponentInParent<PoweredComponent>();
            if (otherComponent != null)
            {
                otherComponent.CheckPower();
            }
        }
    }

    /// <summary>
    /// Checks if this end is currently connected to a socket.
    /// </summary>
    public bool IsConnected()
    {
        return currentSocket != null;
    }

    /// <summary>
    /// Helper method to prevent infinite recursion in IsPowered().
    /// </summary>
    public bool PoweredByDefault()
    {
        return isPowered;
    }
}

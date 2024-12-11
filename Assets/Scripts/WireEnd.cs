using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WireEnd : MonoBehaviour, IPowerProvider
{
    public WireEnd otherEnd; // Set this in the inspector or via script on wire initialization

    private IPowerProvider connectedProvider;
    private XRSocketInteractor currentSocket;

    private void OnEnable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor socket)
        {
            currentSocket = socket;
            // Check if the socket or its parent object has an IPowerProvider
            connectedProvider = socket.GetComponentInParent<IPowerProvider>();

            // Notify connected objects to update power status if needed
            NotifyConnectionChanged();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        currentSocket = null;
        connectedProvider = null;

        // Notify that connection changed
        NotifyConnectionChanged();
    }

    public bool IsPowered()
    {
        // If this end is connected to a power provider directly:
        if (connectedProvider != null && connectedProvider.IsPowered())
        {
            return true;
        }

        // Otherwise, see if other end is powered (this covers the case if power is coming from the other side)
        if (otherEnd != null && otherEnd.connectedProvider != null && otherEnd.connectedProvider.IsPowered())
        {
            return true;
        }

        return false;
    }

    private void NotifyConnectionChanged()
    {
        // If connected to a component that cares about power states, we can notify it here
        // Or rely on a separate component to poll. For this example, let's just do a simple broadcast:
        var component = GetComponentInParent<PoweredComponent>();
        if (component != null)
        {
            component.CheckPower();
        }

        // Notify the other end's parent as well, since a change in this endpoint might affect that:
        if (otherEnd != null && otherEnd.gameObject != null)
        {
            var otherComponent = otherEnd.GetComponentInParent<PoweredComponent>();
            if (otherComponent != null)
            {
                otherComponent.CheckPower();
            }
        }
    }

    public bool IsConnected()
    {
        return currentSocket != null;
    }
}

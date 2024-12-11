using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KinematicControl : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;

    public Rigidbody targetRigidbody;

    private void Start()
    {
        if (targetRigidbody == null)
        {
            targetRigidbody = GetComponent<Rigidbody>();
            if (targetRigidbody == null)
            {
                return;
            }
        }

        // Add listeners for socket events
        if (socket1 != null)
        {
            socket1.selectEntered.AddListener(OnSocketUpdated);
            socket1.selectExited.AddListener(OnSocketUpdated);
        }

        if (socket2 != null)
        {
            socket2.selectEntered.AddListener(OnSocketUpdated);
            socket2.selectExited.AddListener(OnSocketUpdated);
        }

        // Initialize kinematic state
        UpdateKinematicState();
    }

    private void OnDestroy()
    {
        // Remove listeners if objects is destroyed
        if (socket1 != null)
        {
            socket1.selectEntered.RemoveListener(OnSocketUpdated);
            socket1.selectExited.RemoveListener(OnSocketUpdated);
        }

        if (socket2 != null)
        {
            socket2.selectEntered.RemoveListener(OnSocketUpdated);
            socket2.selectExited.RemoveListener(OnSocketUpdated);
        }
    }

    private void OnSocketUpdated(SelectEnterEventArgs args)
    {
        UpdateKinematicState();
    }

    private void OnSocketUpdated(SelectExitEventArgs args)
    {
        UpdateKinematicState();
    }

    // Set object to kinematic if a socket is filled (otherwise physics go crazy)
    private void UpdateKinematicState()
    {
        bool isEitherSocketFilled = (socket1 != null && socket1.hasSelection) ||
                                    (socket2 != null && socket2.hasSelection);

        targetRigidbody.isKinematic = isEitherSocketFilled;

        Debug.Log($"Kinematic state updated: {targetRigidbody.isKinematic}");
    }
}

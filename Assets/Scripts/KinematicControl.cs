using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controls the kinematic state of a Rigidbody based on XRSocketInteractor events.
/// Not used anymore
/// </summary>
public class KinematicControl : MonoBehaviour
{
    public XRSocketInteractor socket1; // First socket interactor
    public XRSocketInteractor socket2; // Second socket interactor

    public Rigidbody targetRigidbody; // Rigidbody to control kinematic state

    private void Start()
    {
        // If target Rigidbody is not assigned, attempt to find it on the current GameObject
        if (targetRigidbody == null)
        {
            targetRigidbody = GetComponent<Rigidbody>();
            if (targetRigidbody == null)
            {
                Debug.LogWarning("Rigidbody component not found or assigned.");
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
        // Remove listeners when the object is destroyed
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

    /// <summary>
    /// Updates the kinematic state of the Rigidbody based socket connection
    /// </summary>
    private void UpdateKinematicState()
    {
        bool isEitherSocketFilled = (socket1 != null && socket1.hasSelection) ||
                                    (socket2 != null && socket2.hasSelection);

        targetRigidbody.isKinematic = isEitherSocketFilled;

        Debug.Log($"Kinematic state updated: {targetRigidbody.isKinematic}");
    }
}

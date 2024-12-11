using UnityEngine;
using UnityEngine.InputSystem; // For InputAction
using UnityEngine.XR.Interaction.Toolkit;

public class MenuToggleOnButtonPress : MonoBehaviour
{
    [Header("Input Action")]

    public InputActionReference toggleMenuActionReference;

    [Header("Menu")]
    public GameObject mainMenu;

    private void OnEnable()
    {
        if (toggleMenuActionReference != null && toggleMenuActionReference.action != null)
        {
            // Subscribe
            toggleMenuActionReference.action.performed += OnMenuTogglePerformed;
        }
    }

    private void OnDisable()
    {
        if (toggleMenuActionReference != null && toggleMenuActionReference.action != null)
        {
            // Unsubscribe
            toggleMenuActionReference.action.performed -= OnMenuTogglePerformed;
        }
    }

    private void OnMenuTogglePerformed(InputAction.CallbackContext context)
    {
        // Toggle the main menu
        if (mainMenu != null)
        {
            bool isActive = mainMenu.activeSelf;
            mainMenu.SetActive(!isActive);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the visibility and position of different menus in the scene.
/// </summary>
public class MenuHandler : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu; // Main menu object
    public GameObject settings; // Settings menu object
    public GameObject debug; // Debug menu object

    [Header("UI Offset")]
    public float distance; // Distance from the camera to position menus

    private List<GameObject> menus = new List<GameObject>(); // List of all menus

    private float uiOffsetRotationY; // Rotation offset for UI menus

    void Start()
    {
        // Calculate the initial rotation offset for UI menus
        uiOffsetRotationY = mainMenu.transform.eulerAngles.y - Camera.main.transform.eulerAngles.y;

        // Add all menus to the list
        menus.Add(mainMenu);
        menus.Add(settings);
        menus.Add(debug);
    }

    /// <summary>
    /// Toggles the visibility of all menus. If any menu is open, it closes all; otherwise, it opens the main menu.
    /// </summary>
    public void Toggle()
    {
        if (AnyOpen())
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>
    /// Checks if any menu is currently open.
    /// </summary>
    /// <returns>True if any menu is active, false otherwise.</returns>
    private bool AnyOpen()
    {
        foreach (GameObject menu in menus)
        {
            if (menu.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Opens the main menu and positions it in front of the camera at a specified distance.
    /// </summary>
    private void Open()
    {
        Vector3 uiPos = Camera.main.transform.position + (Camera.main.transform.forward * distance);
        uiPos.y = Camera.main.transform.position.y;

        Quaternion uiRot = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
            menu.transform.position = uiPos;
            menu.transform.rotation = uiRot;
        }
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// Closes all open menus.
    /// </summary>
    private void Close()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }
}

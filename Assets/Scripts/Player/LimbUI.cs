using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;

public class LimbUI : MonoBehaviour
{
    public GameObject inventary;
    public GameObject pauseMenu;
    public CinemachinePanTilt camrot;

    [HideInInspector]public bool isPaused;
    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }

        if (Input.GetButtonDown("Escape"))
        {
            TogglePause();
        }
    }
    void ToggleInventory()
    {
        bool isActive = inventary.activeSelf;
        inventary.SetActive(!isActive);

        if (!isActive)
        {
            camrot.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            camrot.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        camrot.enabled = !isPaused;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}

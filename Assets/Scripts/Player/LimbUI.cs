using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LimbUI : MonoBehaviour
{
    public GameObject inventary;
    public Camera camera;

    private void Start()
    {
    
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            bool isActive = inventary.activeSelf;
            inventary.SetActive(!isActive);

            if (!isActive)
            {
                camera.CinemachinePanTilt.enable = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                camera.CinemachinePanTilt.enable = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}

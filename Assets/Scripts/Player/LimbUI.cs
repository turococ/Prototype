using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;

public class LimbUI : MonoBehaviour
{
    public GameObject inventary;
    public CinemachinePanTilt camrot;

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
                camrot.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                camrot.enabled = true;
            }
        }
    }
}

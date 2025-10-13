using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] public float mouseSens = 400f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        xRotation -= mouseY;
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void OnApplicationFocus(bool hasFocus) 
    {
        if (hasFocus)
            Cursor.lockState = CursorLockMode.Locked;
    }
}

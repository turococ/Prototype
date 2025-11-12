using System.Linq;
using Unity.Cinemachine;
using UnityEngine;



public class CameraController : MonoBehaviour
{
    private Transform cam;
    private Transform Legs;

    void Start()
    {
        cam = Camera.main.transform;
        Legs = transform.Find("Legs");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Legs.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
    }
}
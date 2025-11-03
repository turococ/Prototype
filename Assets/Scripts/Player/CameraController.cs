using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;



public class CameraController : MonoBehaviour
{
    private float crouchDamping = 0.3f;
    private float defaultDamping = 0f;

    private PlayerInput input;
    private CharacterController controller;
    private CinemachineHardLockToTarget cam;
    void Start()
    {
        cam = GetComponentInChildren<CinemachineHardLockToTarget>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!controller.isGrounded)
        {
            cam.Damping = defaultDamping;
        }
        else if (input.Run)
        {
            cam.Damping = defaultDamping;
        }
        else
        {
            cam.Damping = crouchDamping;
        }
        //cam.Damping = !fly ? crouchDamping : defaultDamping;
        //cam.Damping = input.Run ? defaultDamping : crouchDamping;
    }
}
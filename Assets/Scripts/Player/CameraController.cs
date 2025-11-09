using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Windows;



public class CameraController : MonoBehaviour
{
    private float crouchDamping = 0.3f;
    private float runDamping = 0.1f;
    private float defaultDamping = 0f;

    private PlayerInput input;
    private CharacterController controller;
    private CinemachineHardLockToTarget cam;
    private CinemachineBasicMultiChannelPerlin bobhead;
    void Start()
    {
        cam = GetComponentInChildren<CinemachineHardLockToTarget>();
        bobhead = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!controller.isGrounded)
        {
            cam.Damping = defaultDamping;
        }
        else if (input.Run && !input.Crouch)
        {
            cam.Damping = runDamping;
            
        }
        else
        {
            cam.Damping = crouchDamping;
        }
    }
}
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchDamping = 0.3f;
    public float defaultDamping = 0f;
    private float crouchHeight = 1f;
    private float crouchSpeed = 8f;
    private float originalHeight;
    private float velocity;

    private PlayerInput input;
    private CharacterController controller;
    private CinemachineHardLockToTarget cam;

    


    void Start()
    {
        cam = GetComponentInChildren<CinemachineHardLockToTarget>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();

        originalHeight = controller.height;
    }

    private void Update()
    {
        controller.height = input.Crouch ? crouchHeight : originalHeight;
        //controller.height = Mathf.SmoothDamp(controller.height, originalHeight, ref velocity, 1f / crouchSpeed);
        //controller.center = new Vector3(controller.center.x, controller.height / 2f, controller.center.z);
    }
}

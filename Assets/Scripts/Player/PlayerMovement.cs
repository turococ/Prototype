using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpHeight;

    private CharacterController controller;
    private float verticalVelocity;
    private PlayerInput input;
    private Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y  = right.y = 0;

        Vector3 move = (forward * input.Move.y + right * input.Move.x);

        float currentSpeed = input.Run && !input.Crouch ? runSpeed : walkSpeed;
        move *= currentSpeed;

        if (controller.isGrounded) 
        {
            verticalVelocity = -1;
            if (input.Jump)
                verticalVelocity = math.sqrt(jumpHeight * 2 * 9.81f);
        }
        else
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}

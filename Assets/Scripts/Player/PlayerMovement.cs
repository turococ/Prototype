using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpHeight;

    public float currentSpeed;

    private CharacterController controller;

    private float verticalVelocity;
    private float gravity = 9.81f;

    [SerializeField] public float impactVelocity;

    [HideInInspector]public PlayerInput input;
    [HideInInspector]public Transform cam;

    private Body body;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main.transform;
        body = GetComponent<Body>();
    }

    void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        Vector2 moveInput = input.Move;
        Vector3 move = forward * moveInput.y + right * moveInput.x;

        currentSpeed = input.Run && !input.Crouch ? runSpeed : walkSpeed;

        bool broken_LL = body.brokenLL();
        bool broken_RL = body.brokenRL();

        if (broken_LL || broken_RL)
        {
            currentSpeed *= 0.5f;
        }
        if (broken_LL && broken_RL)
        {
            currentSpeed *= 0.4f;
        }

        move *= currentSpeed;

        if (controller.isGrounded) 
        {
            if (verticalVelocity < 0)
                verticalVelocity = -5;

            if (input.Jump)
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);

            impactVelocity = 0;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            impactVelocity = verticalVelocity;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}

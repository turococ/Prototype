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
    [SerializeField] public float impactVelocity;
    private PlayerInput input;
    private Transform cam;
    private Body body;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main.transform;
        body = GetComponent<Body>();
    }

    void Update()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y  = right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * input.Move.y + right * input.Move.x);

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
            verticalVelocity = -1;
            if (input.Jump)
                verticalVelocity = math.sqrt(jumpHeight * 2 * 9.81f);
            impactVelocity = 0;
        }
        else
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
            impactVelocity = verticalVelocity;
        }

        

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}

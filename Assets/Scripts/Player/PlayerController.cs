using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    public float runSpeed = 7.0f;
    public float jumpHeight = 1.5f;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 9.81f;

    public Transform cameraTransform; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetButton("Run") ? runSpeed : speed;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * moveVertical + right * moveHorizontal) * currentSpeed;

        if (controller.isGrounded)
        {
            moveDirection.y = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = math.sqrt(jumpHeight * 2.0f * gravity);
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        Vector3 finalMove = new Vector3(move.x, moveDirection.y, move.z);

        controller.Move(finalMove * Time.deltaTime);
    }
}

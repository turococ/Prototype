using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float runSpeed = 5.5f;
    public float CrouchSpeed = 1.0f;
    public float jumpHeight = 0.7f;
    public float crouchHeight = 1f;


    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 9.81f;
    private float OriginalSpeed;
    private bool IsCrouch = false;
    private float OriginalHeight;


    public Transform cameraTransform;

    void Start()
    {
        OriginalSpeed = speed;

        OriginalHeight = GetComponent<CharacterController>().height;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //DebugLog
        Debug.Log(IsCrouch);
        Debug.Log(speed);

        //����������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetButton("Run") ? runSpeed : speed;

        //�������� � ������� ���� ������� ������
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //��������� ������������
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * moveVertical + right * moveHorizontal) * currentSpeed;

        //����������
        if (Input.GetButton("crouch"))
        {
            if (!IsCrouch)
                crouch(true);
        }
        else
        {
            if (IsCrouch)
                crouch(false);
        }


        //������������ ������
        if (controller.isGrounded)
        {
            moveDirection.x = move.x;
            moveDirection.z = move.z;
            moveDirection.y = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = math.sqrt(2 * gravity * jumpHeight);
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //����������� ������������ � �������
        //Vector3 finalMove = new Vector3(move.x, moveDirection.y, move.z);

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void crouch(bool crouch)
    {
        

        IsCrouch = crouch;

        if (crouch)
        {
            controller.height = crouchHeight;
            speed = CrouchSpeed;
        }
        else
        {
            controller.height = OriginalHeight;
            speed = OriginalSpeed;
        }
    }
}

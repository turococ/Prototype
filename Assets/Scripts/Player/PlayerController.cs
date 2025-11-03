using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float runSpeed = 5.5f;
    public float CrouchSpeed = 1.0f;
    public float jumpHeight = 0.7f;
    public float crouchHeight = 1f;
    public GameObject cm;
    public float dampingCrouch = 0.3f;
    public float dampingOriginal = 0f;
    

    private CharacterController controller;
    
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 9.81f;
    private float OriginalSpeed;
    private bool IsCrouch = false;
    private float OriginalHeight;
    public Transform cameraTransform;
    private CinemachineHardLockToTarget dam;

    void Start()
    {
        OriginalSpeed = speed;

        OriginalHeight = GetComponent<CharacterController>().height;
        controller = GetComponent<CharacterController>();
        dam = GetComponentInChildren<CinemachineHardLockToTarget>();
    }

    void Update()
    {
        //DebugLog
        Debug.Log(IsCrouch);
        Debug.Log(speed);
        Debug.Log(dam.Damping);

        //переменные
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetButton("Run") ? runSpeed : speed;

        //движение в сторону куда смотрит камера
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //настройки передвижения
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * moveVertical + right * moveHorizontal) * currentSpeed;

        //приседание
        if (Input.GetButton("crouch"))
        {
            if (!IsCrouch)
                crouch(true);
            dam.Damping = controller.isGrounded ? dampingCrouch : dampingOriginal;
        }
        else
        {
            if (IsCrouch)
                crouch(false);
        }

        

        //передвижение игрока
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

        //возможность передвижения в воздухе
        //Vector3 finalMove = new Vector3(move.x, moveDirection.y, move.z);

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void crouch(bool crouch)
    {
        

        IsCrouch = crouch;

        if (crouch)
        {
            StartCoroutine(Clock());
            controller.height = crouchHeight;
            speed = CrouchSpeed;
        }
        else
        {          
            controller.height = OriginalHeight;
            speed = OriginalSpeed;           
        }
    }
    IEnumerator Clock()
    {
        while(true)
        {
            dam.Damping = dampingOriginal;
            yield return new WaitForSeconds(2);
        }
    }

}

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //скорость передвижения
    public float walkSpeed; //скорость ходьбы
    public float runSpeed; // скорость бега
    public float crouchSpeed; // скорость в приседании
    public float jumpHeight; // высота прыжка

    public float currentSpeed; // текущая скорость игрока

    private CharacterController controller; // компонент character controller

    private float verticalVelocity; // вертикальная скорость(нужна для прыжка и гравитации)
    private float gravity = 9.81f; // гравитация

    [SerializeField] public float impactVelocity; // скорость падения(используется для вычисления перелома ног при падении)

    [HideInInspector]public PlayerInput input; // система ввода игрока
    [HideInInspector]public Transform cam; // камера игрока

    private Body body; // обращение к скрипту тела игрока

    void Awake()
    {
        // получение необходимых компонентов
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main.transform; // получаем располежение камеры
        body = GetComponent<Body>();
    }

    void Update()
    {
        // направдениея движения игрока относительно камеры
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        // ввод движения (WASD / gamepad stick)
        Vector2 moveInput = input.Move;
        Vector3 move = forward * moveInput.y + right * moveInput.x; // вектор движения

        currentSpeed = input.Run ? runSpeed : input.Crouch ? crouchSpeed : walkSpeed; // определение скорости (бег, приседание или ходьба)

        // проверка состояния ног
        bool broken_LL = body.brokenLL();
        bool broken_RL = body.brokenRL();

        //замедление при травмах
        if (broken_LL || broken_RL)
        {
            currentSpeed *= 0.5f;
        }
        if (broken_LL && broken_RL)
        {
            currentSpeed *= 0.4f;
        }

        // применение скорости
        move *= currentSpeed;

        // проверка нахождения на земле
        if (controller.isGrounded) 
        {
            // небольшое прижатие к земле
            if (verticalVelocity < 0)
                verticalVelocity = -5;

            // прыжок
            if (input.Jump)
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);

            // сброс скорости подения
            impactVelocity = 0;
        }
        else
        {
            // применения гравитации
            verticalVelocity -= gravity * Time.deltaTime;
            impactVelocity = verticalVelocity;
        }

        move.y = verticalVelocity; // вертикальное движение
        controller.Move(move * Time.deltaTime); // передвижение персонажа
    }
}

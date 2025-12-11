using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
   
    private float crouchHeight = 1f;
    private float originalHeight;
    private bool staypos;

    private Body body;
    private PlayerInput input;
    private CharacterController controller;
    
    void Awake()
    {
        body = GetComponent<Body>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        

        originalHeight = controller.height;
    }

    private void Update()
    {
        bool broken_LL = body.brokenLL();
        bool broken_RL = body.brokenRL();

        if (input.Crouch || broken_LL && broken_RL)
        {
            controller.height = crouchHeight;
            staypos = true;
        }
        else if (staypos == true)
        {
            controller.height = originalHeight;
        }
    }

   
}

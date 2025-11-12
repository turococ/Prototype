using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
   
    private float crouchHeight = 1f;
    private float originalHeight;
    private bool staypos;

    private PlayerInput input;
    private CharacterController controller;
    
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        

        originalHeight = controller.height;
    }

    private void Update()
    {
        if (input.Crouch)
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

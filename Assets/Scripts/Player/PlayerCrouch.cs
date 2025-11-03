using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
   
    private float crouchHeight = 1f;
    private float originalHeight;

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
        controller.height = input.Crouch ? crouchHeight : originalHeight;
        
    }
}

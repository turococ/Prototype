using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    public bool Run { get; private set; }

    void Update() 
    {
        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Jump = Input.GetButtonDown("Jump");
        Crouch = Input.GetButton("crouch");
        Run = Input.GetButton("Run");
    }
}

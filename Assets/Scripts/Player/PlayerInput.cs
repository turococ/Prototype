using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    public bool Run { get; private set; }

    private Body body;

    void Awake()
    {
        body = GetComponent<Body>();
    }

    void Update() 
    {
        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Jump = Input.GetButtonDown("Jump");
        Crouch = Input.GetButton("crouch");

        if (body.brokenLL() && body.brokenRL())
        {
            Run = false;
        }
        else
            Run = Input.GetButton("Run");
    }
}

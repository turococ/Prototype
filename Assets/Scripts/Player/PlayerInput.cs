using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Crouch { get; private set; }
    public bool Run { get; private set; }
    public bool Inventory {get; private set;}
    public bool Escape { get; private set;}

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
        Escape = Input.GetButton("Escape");
        Inventory = Input.GetButton("Inventory");

        if (body.brokenLL() && body.brokenRL() || Crouch)
        {
            Run = false;
        }
        else
            Run = Input.GetButton("Run");
    }
}

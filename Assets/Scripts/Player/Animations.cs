using UnityEngine;

public class Animations : MonoBehaviour
{
    public float smooth = 0.05f;

    public Animator camAnim;
    public Animator Player;
    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 move = input.Move;

        if (Input.GetButton("Vertical"))
        {
            Player.SetFloat("moveZ", Mathf.Lerp(Player.GetFloat("moveZ"), move.y, smooth));
        }
        else
        {
            Player.SetFloat("moveZ", Mathf.Lerp(Player.GetFloat("moveZ"), 0, smooth));
        }

        //наклон камеры
        if (Input.GetButton("Horizontal"))
        {
            Player.SetFloat("moveX", Mathf.Lerp(Player.GetFloat("moveX"), move.x, smooth));
            camAnim.SetFloat("left||right", Mathf.Lerp(camAnim.GetFloat("left||right"), move.x, smooth));
        }
        else
        {
            Player.SetFloat("moveX", Mathf.Lerp(Player.GetFloat("moveX"), 0, smooth));
            camAnim.SetFloat("left||right", Mathf.Lerp(camAnim.GetFloat("left||right"), 0, smooth));
        }
    }
}

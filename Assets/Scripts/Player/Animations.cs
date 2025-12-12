using UnityEngine;

public class Animations : MonoBehaviour
{
    public float smooth = 0.05f;

    public Animator camAnim;
    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector2 move = input.Move;

        //наклон камеры
        if (Input.GetButton("Horizontal"))
        {
            camAnim.SetFloat("left||right", Mathf.Lerp(camAnim.GetFloat("left||right"), move.x, smooth));
        }
        else
        {
            camAnim.SetFloat("left||right", Mathf.Lerp(camAnim.GetFloat("left||right"), 0, smooth));
        }
    }
}

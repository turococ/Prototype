using UnityEngine;

public class Animations : MonoBehaviour
{
    public float smooth = 0.05f;

    private Animator animatorLegs;
    private Animator animatorFull;
    private PlayerInput input;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        animatorLegs = GetComponent<Animator>();
        animatorFull = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector2 move = input.Move;

        //ходьба
        if (Input.GetButton("Vertical"))
        {
            animatorLegs.SetFloat("moveZ", Mathf.Lerp(animatorLegs.GetFloat("moveZ"), move.y, smooth));
            animatorFull.SetFloat("moveZ", Mathf.Lerp(animatorLegs.GetFloat("moveZ"), move.y, smooth));
        }
        else
        {
            animatorLegs.SetFloat("moveZ", Mathf.Lerp(animatorLegs.GetFloat("moveZ"), 0, smooth));
            animatorFull.SetFloat("moveZ", Mathf.Lerp(animatorLegs.GetFloat("moveZ"), 0, smooth));
        }

        if (Input.GetButton("Horizontal"))
        {
            animatorLegs.SetFloat("moveX", Mathf.Lerp(animatorLegs.GetFloat("moveX"), move.x, smooth));
            animatorFull.SetFloat("moveX", Mathf.Lerp(animatorLegs.GetFloat("moveX"), move.x, smooth));
        }
        else
        {
            animatorLegs.SetFloat("moveX", Mathf.Lerp(animatorLegs.GetFloat("moveX"), 0, smooth));
            animatorFull.SetFloat("moveX", Mathf.Lerp(animatorLegs.GetFloat("moveX"), 0, smooth));
        }

        //Бег
        animatorLegs.SetBool("Run", input.Run);

        animatorLegs.SetBool("Crouch", input.Crouch); //приседание
    }
}

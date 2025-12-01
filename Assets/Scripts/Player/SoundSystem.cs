using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public AudioClip Fall_wind;


    //flags
    private bool IsFalling;

    private AudioSource audiosys;
    private PlayerMovement move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosys = GetComponent<AudioSource>();
        move = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFalling && move.impactVelocity < -3f)
        {
            audiosys.PlayOneShot(Fall_wind);
            IsFalling = true;
        }
        if (move.impactVelocity >= -1f)
        {
            IsFalling = false;
            audiosys.Stop();
        }
    }
}

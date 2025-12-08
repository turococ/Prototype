using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public AudioClip Fall_wind;

    private AudioSource audioSource;
    private PlayerMovement move;

    [Header("Thresholds")]
    public float FallinVelocity = -3f;
    public float IsGroundVelocity = -1f;

    private bool IsFalling;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        move = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float IV = move.impactVelocity;

        if (!IsFalling && IV < FallinVelocity)
        {
            IsFalling = true;
            audioSource.clip = Fall_wind;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (IsFalling && IV > IsGroundVelocity)
        {
            IsFalling = false;
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}

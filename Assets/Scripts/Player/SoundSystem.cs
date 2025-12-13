using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public AudioClip Fall_wind;

    [Header("Footsteps")]
    public AudioClip Tile1;
    public AudioClip Tile2;
    public AudioClip Tile3;

    public float stepDelay = 0.5f;

    [Header("Thresholds")]
    public float FallingVelocity = -3f;
    public float IsGroundVelocity = -1f;

    private AudioSource audioSource;
    private PlayerInput playerInput;
    private PlayerMovement Player;
    private CharacterController controller;

    private AudioSource fallSource;
    private AudioSource StepSource;

    private bool IsFalling;
    private bool isStepping;
    private int lastStep = -1;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Player = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();

        AudioSource[] sources = GetComponents<AudioSource>();
        fallSource = sources[0];
        StepSource = sources[1];
    }

    void Update()
    {
        HandleFallingSound();
        HandleFootsteps();
    }
    void HandleFallingSound()
    {
        float impactVelocity = Player.impactVelocity;

        if (!IsFalling && impactVelocity < FallingVelocity)
        {
            IsFalling = true;
            fallSource.clip = Fall_wind;
            fallSource.loop = true;
            fallSource.Play();
        }
        else if (IsFalling && impactVelocity > IsGroundVelocity)
        {
            IsFalling = false;
            fallSource.Stop();
            fallSource.clip = null;
        }
    }
    void HandleFootsteps()
    {
        if (playerInput.Move.magnitude > 0.1f &&
            controller.isGrounded &&
            !isStepping)
        {
            StartCoroutine(FootSteps());
        }
        if (playerInput.Run)
        {
            stepDelay = 0.2f;
        }
        else
            stepDelay = 0.5f;
    }

    IEnumerator FootSteps()
    {
        isStepping = true;

        while (playerInput.Move.magnitude > 0.1f && controller.isGrounded)
        {
            int step;
            do
            {
                step = Random.Range(1, 5);
            }
            while (step == lastStep);

            lastStep = step;

            switch (step)
            {
                case 1: StepSource.clip = Tile1; break;
                case 2: StepSource.clip = Tile2; break;
                case 3: StepSource.clip = Tile3; break;
            }

            StepSource.Play();
            yield return new WaitForSeconds(stepDelay);
        }

        isStepping = false;
    }
}

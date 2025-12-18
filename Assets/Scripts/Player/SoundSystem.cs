using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public enum SurfaceType
    {
        Tile,
        chainlink
    }

    public AudioClip Fall_wind;

    [Header("Footsteps")]
    public AudioClip[] TileSteps;
    public AudioClip[] ChainSteps;

    [Header("SoundDelay")]
    public float WalkDelay = 0.5f;
    public float RunDelay = 0.25f;
    public float CrouchDelay = 0.75f;

    private float stepDelay;

    [Header("Thresholds")]
    public float FallingVelocity = -3f;
    public float IsGroundVelocity = -1f;

    private AudioSource audioSource;
    private PlayerInput playerInput;
    private PlayerMovement Player;
    private CharacterController controller;

    private AudioSource fallSource;
    private AudioSource StepSource;

    [SerializeField] private SurfaceType currentSurface;
    [SerializeField] private LayerMask groundmask;

    public float SurfaceCheckDistance = 1.2f;

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
        DetectSurface();
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
        stepDelay = playerInput.Run ? RunDelay : playerInput.Crouch ? CrouchDelay : WalkDelay;

        if (playerInput.Move.magnitude > 0.1f &&
            controller.isGrounded &&
            !isStepping)
        {
            StartCoroutine(FootSteps());
        }
    }

    IEnumerator FootSteps()
    {
        isStepping = true;

        while (playerInput.Move.magnitude > 0.1f && controller.isGrounded)
        {
            currentSurface = DetectSurface();

            AudioClip[] clips = currentSurface switch
            {
                SurfaceType.Tile => TileSteps,
                SurfaceType.chainlink => ChainSteps,
                _ => TileSteps
            };

            int index;
            do
            {
                index = Random.Range(0, clips.Length);
            }
            while (index == lastStep);

            lastStep = index;

            StepSource.clip = clips[index];
            StepSource.Play();

            yield return new WaitForSeconds(stepDelay);
        }

        isStepping = false;
    }

    SurfaceType DetectSurface()
    { 
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(origin, Vector3.down, out hit, SurfaceCheckDistance, groundmask))
        {
            if (hit.collider.CompareTag("chainlink"))
                return SurfaceType.chainlink;
        }

        return SurfaceType.Tile;
    }
}

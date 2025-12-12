using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    float crouchHeight = 1f;
    public float crouchTransitionSpeed = 10f;

    private Body body;
    private PlayerInput input;
    private CharacterController controller;

    float StandingHeight;
    float CurrentHeight;

    bool IsCrouching => StandingHeight - CurrentHeight > .1f;

    void Awake()
    {
        body = GetComponent<Body>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        StandingHeight = CurrentHeight = controller.height;
    }

    private void Update()
    {
        bool broken_LL = body.brokenLL();
        bool broken_RL = body.brokenRL();

        float crouchDelta = Time.deltaTime * crouchTransitionSpeed;

        if (input.Crouch || broken_LL && broken_RL)
        {
            float heightTarget = crouchHeight;
            CurrentHeight = Mathf.Lerp(CurrentHeight, heightTarget, crouchDelta);
            controller.height = CurrentHeight;
        }
        else
        {
            float heightTarget = StandingHeight;
            
            if (IsCrouching && !input.Crouch)
            {
                var castOrigin = transform.position + new Vector3(0, CurrentHeight /  2, 0);
                if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
                {
                    var distanceToCeiling = hit.point.y - castOrigin.y;
                    heightTarget = Mathf.Max
                    (
                        CurrentHeight + distanceToCeiling - 0.1f,
                        crouchHeight
                    );
                }
            }

            CurrentHeight = Mathf.Lerp(CurrentHeight, heightTarget, crouchDelta);
            controller.height = CurrentHeight;
        }
    }
}

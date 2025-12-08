using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Limb
{
    public string name;

    [Header("Health")]
    public float maxHealth = 20f;
    public float health = 20f;

    [Header("Head sickness")]
    public float brain_damage;
    public bool neck_break;

    [Header("Injuries")]
    public bool broken;
    public bool dislocate;

    [Header("Status")]  
    public bool isBleeding;

}
public class Body : MonoBehaviour
{
    public float blood = 3f;
    public float bleedRate = 0.01f;
    public bool IsPain = false;

    private CharacterController controller;
    private PlayerInput input;
    private PlayerMovement move;
    private pain Pain;

    public List<Limb> limbs = new List<Limb> ();
    
    public LimbUI limbUIPr;
    public Transform uiConteiner;
    private void Awake()
    {
        controller = GetComponent<CharacterController> ();
        input = GetComponent<PlayerInput> ();
        move = GetComponent<PlayerMovement> ();
        Pain = GetComponent<pain> ();

        foreach (var limb in limbs)
        {
            LimbUI ui = Instantiate(limbUIPr, uiConteiner);
            ui.Setup(limb);
        }
    }

    private void Update()
    {
        if (IsPain == false && Pain.PainValue != 0)
        {
            Pain.PainValue = Mathf.Lerp(Pain.PainValue, 0f, 0.00025f);
        }

        if (controller.isGrounded)
        {
            if (move.impactVelocity <= -14f)
            {
                Broke();
            }
        }
        foreach (var limb in limbs)
        {
            if (limb.isBleeding)
            {
                blood = Mathf.Max(blood, 0);
                blood -= Time.deltaTime * bleedRate;
            }
        }

        if (brokenLL() || brokenRL())
        {
            if (input.Run)
            {
                IsPain = true;
                Pain.PainValue = Mathf.Lerp(Pain.PainValue, 0.5f, 0.05f);
            }
            else
                IsPain = false;
        }
    }

    private void Broke()
    {
        bool brokenLeft = brokenLL();
        bool brokenRight = brokenRL();

        int[] RightLeg = { 9, 10, 11 };

        int[] LeftLeg = { 12, 13, 14 };

        int[] selected;

        if (brokenLeft && !brokenRight)
            selected = RightLeg;
        else if (brokenRight && !brokenLeft)
            selected = LeftLeg;
        else if (!brokenLeft && !brokenRight)
        {
            int value = Random.Range(0, 100);
            selected = value <= 50 ? LeftLeg : RightLeg;
        }
        else
            return;

        foreach (int i in selected)
        {
            limbs[i].health = 0;
            limbs[i].broken = true;
            limbs[i].isBleeding = true;
        }
        move.impactVelocity = 0;
    }

    public bool brokenLL()
    {
        int[] leftLeg = { 12, 13, 14 };
        foreach (int i in leftLeg)
            if (limbs[i].broken) return true;
        return false;
    }

    public bool brokenRL()
    {
        int[] rightLeg = { 9, 10, 11 };
        foreach (int i in rightLeg)
            if (limbs[i].broken) return true;
        return false;
    }

    public bool AreBothLegsBroken()
    {
        return brokenLL() && brokenRL();
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LimbUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text LimbNameText;
    public Slider Limbhealth;
    public Image brokenIcon;

    public TMP_Text blood;

    private Limb limb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Limb limbRef)
    {
        limb = limbRef;
        LimbNameText.text = limb.name;
        Limbhealth.maxValue = limb.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (limb == null) return;

        Limbhealth.value = limb.health;
        brokenIcon.enabled = limb.broken;
        //blood.text = limb.blood;
    }
}

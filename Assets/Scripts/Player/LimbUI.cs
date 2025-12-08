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

    private float lastHealth;
    private bool lastBroken;

    public void Setup(Limb limbRef)
    {
        limb = limbRef;
        LimbNameText.text = limb.name;
        Limbhealth.maxValue = limb.maxHealth;

        ForceUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (limb == null) return;

        if (!Mathf.Approximately(limb.health, lastHealth))
        {
            Limbhealth.value = limb.health;
            lastHealth = limb.health;
        }
        if (limb.broken != lastBroken)
        {
            brokenIcon.enabled = limb.broken;
            lastBroken = limb.broken;
        }
    }

    private void ForceUpdate()
    {
        lastHealth = -1;
        lastBroken = !limb.broken;

        Limbhealth.value = limb.health;
        brokenIcon.enabled = limb.broken;
    }
}

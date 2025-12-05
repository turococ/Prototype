using UnityEngine;
using System.Collections;

public class pain : MonoBehaviour
{
    public Material targetMaterial;
    public string Gainname = "_gain";

    public float PainValue = 1f;
    public float impulse = 0.3f;
    public float speed = 1f;

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * impulse;
        float currentValue = PainValue + offset;

        targetMaterial.SetFloat(Gainname, currentValue);
    }
}
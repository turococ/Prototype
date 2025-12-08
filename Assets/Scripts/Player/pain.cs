using UnityEngine;
using System.Collections;

public class pain : MonoBehaviour
{
    public Material targetMaterial;
    public string Gainname = "_gain";

    public float PainValue = 1f;
    public float impulse = 0.3f;
    public float speed = 1f;

    private int gainID;
    private float lastValume;

    private void Awake()
    {
        gainID = Shader.PropertyToID(Gainname);
    }

    private void Update()
    {
        if (targetMaterial == null) return;

        float currentValue = PainValue + Mathf.Sin(Time.time * speed) * impulse;

        if (!Mathf.Approximately(currentValue, lastValume))
        {
            targetMaterial.SetFloat(Gainname, currentValue);
            lastValume = currentValue;
        }
    }
}
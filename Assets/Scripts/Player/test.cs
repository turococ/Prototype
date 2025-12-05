using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    [Header("Материал")]
    public Material targetMaterial;

    [Header("Настройки колебания")]
    public float duration = 1f;
    public AnimationCurve gainCurve = AnimationCurve.Linear(0, 2, 1, 0);
    public string Gainname = "_gain";

    void Start()
    {
        if (targetMaterial != null)
        {
            targetMaterial.SetFloat(Gainname, gainCurve.Evaluate(0));
        }

        StartCoroutine(Pain());
    }

    private IEnumerator Pain()
    {
        float time = 0f;

        while (time < duration) 
        {
            
             
            float normalizedTime = time / duration;
            float gainValue = gainCurve.Evaluate(normalizedTime);
            targetMaterial.SetFloat(Gainname, gainValue);
            time += Time.deltaTime;
             

            /*if (targetMaterial != null)
            {
                targetMaterial.SetFloat(Gainname, gainCurve.Evaluate(0));
            }*/

           
            yield return null;
        }
        targetMaterial.SetFloat(Gainname, 0);
    }
}
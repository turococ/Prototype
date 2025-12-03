using UnityEngine;

public class test : MonoBehaviour
{
    [Header("Материал")]
    public Material targetMaterial;

    [Header("Настройки колебания")]
    public float duration = 2f; // сколько секунд на полный цикл
    public AnimationCurve gainCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // можно рисовать кривую в инспекторе
    public float minGain = 0f;
    public float maxGain = 2f;

    private float time = 0f;

    private void Update()
    {
        if (targetMaterial == null) return;

        // нормализуем время 0..1
        time += Time.deltaTime;
        float t = (time % duration) / duration;

        // вычисляем значение по кривой
        float curveValue = gainCurve.Evaluate(t);

        // переводим 0..1 в нужный диапазон
        float gain = Mathf.Lerp(minGain, maxGain, curveValue);

        // применяем к материалу
        targetMaterial.SetFloat("_Gain", gain);
    }
}

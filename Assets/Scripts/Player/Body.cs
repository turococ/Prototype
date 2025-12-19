using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// данные одной конечности персонажа
[System.Serializable]
public class Limb
{
    public string name;     // название конечности
    public Image LimbImage;     // UI-кнопки конечности

    [Header("Health")]
    public float maxHealth = 20f;   // максимальное здоровье конечности
    public float health = 20f;  // здоровье конечности

    [Header("Head sickness")]
    public float brain_damage;  // повреждение мозга (актуально для головы)
    public bool neck_break;     // перелом шеи

    [Header("Injuries")]
    public bool broken;     //сломана ли конечность
    public bool dislocate;      //Вывих

    [Header("Status")]  
    public bool isBleeding;     // идёт ли кровотечение
}

/* управление состоянием тела игрока:
   кровью, болью, травмами и их последствиями*/
public class Body : MonoBehaviour
{
    public float hunger = 100;      // голод
    public float blood = 3f;        // общее количество урови у персонажа
    public float bleedRate = 0.01f;     // скорость потери крови при кровотечении
    public bool IsPain = false;     // испытывает ли персонаж боль в данный момент

    // ссылики на компоненты
    private CharacterController controller;
    private PlayerInput input;
    private PlayerMovement move;
    private pain Pain;      // компонент боли для взаимодействия с UI

    public List<Limb> limbs = new List<Limb> ();        // массив всех конечностей
    
    private void Awake()
    {
        //получение необходимых компонентов
        controller = GetComponent<CharacterController> ();
        input = GetComponent<PlayerInput> ();
        move = GetComponent<PlayerMovement> ();
        Pain = GetComponent<pain> ();
    }

    private void Update()
    {
        // постепенное уменьшение боли, если игрок не испытывает боль
        if (IsPain == false && Pain.PainValue != 0)
        {
            Pain.PainValue = Mathf.Lerp(Pain.PainValue, 0f, 0.00025f);
        }

        // проверка падения с большой высоты
        if (controller.isGrounded)
        {
            if (move.impactVelocity <= -14f)
            {
                Broke();  // перелом конечности при сильном ударе об пол
            }
        }

        // обработка кровотечения и обновления UI
        foreach (var limb in limbs)
        {
            if (limb.isBleeding)
            {
                blood = Mathf.Max(blood, 0);
                blood -= Time.deltaTime * bleedRate;
            }

            UpdateColor(limb);  // обновление цвета для конечности
        }

        // усиление боли при беге с переломанными ногами
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

    // Ломает одну из ног при сильном падении
    private void Broke()
    {
        bool brokenLeft = brokenLL();
        bool brokenRight = brokenRL();

        //индексы правой и левой ноги в массиве конечностей
        int[] RightLeg = { 9, 10, 11 };

        int[] LeftLeg = { 12, 13, 14 };

        int[] selected;

        // выбор ноги для перелома
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
            return;  // обе ночи уже сломаны

        // применение перелома ко всем частям выбранной ноги
        foreach (int i in selected)
        {
            limbs[i].health = 0;
            limbs[i].broken = true;
            limbs[i].isBleeding = true;
        }
        
        // сбрасываем скорость удара, чтобы не ломать постоянно ноги
        move.impactVelocity = 0;
    }

    // проверка сломана ли левая нога
    public bool brokenLL()
    {
        int[] leftLeg = { 12, 13, 14 };
        foreach (int i in leftLeg)
            if (limbs[i].broken) return true;
        return false;
    }

    // проверка сломана ли правая нога
    public bool brokenRL()
    {
        int[] rightLeg = { 9, 10, 11 };
        foreach (int i in rightLeg)
            if (limbs[i].broken) return true;
        return false;
    }

    //проверка сломаны ли обе ноги
    public bool AreBothLegsBroken()
    {
        return brokenLL() && brokenRL();
    }

    // обновление цвета иконки конечности в UI
    public void UpdateColor(Limb limb)
    {
        // чёрный цвет - перелом
        if (limb.broken) 
        {
            limb.LimbImage.color = Color.black;
        }
        // градиент от белого к красному - урон
        else if (limb.health < limb.maxHealth)
        {
            float damagePercent = 1f - (limb.health / limb.maxHealth);
            limb.LimbImage.color = Color.Lerp(Color.white, Color.red, damagePercent);
        }
        // белый - полностью здоров
        else
        {
            limb.LimbImage.color = Color.white;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text unitNameText;
    public Text unitLevelText;

    public Slider unitHPSlider;
    public Slider unitMPSlider;
    public Slider unitXpSlider;

    public Text unitHpDisplayText;

    private float unitMaxHp;

    public void UpdateHUD(Unit unit)
    {
        unitNameText.text = unit.unitName;
        unitLevelText.text = $"Lv. {unit.unitLevel}";
        unitHPSlider.maxValue = unit.maxHP;
        unitHPSlider.value = unit.currentHP;
        unitMPSlider.maxValue = unit.maxMP;
        unitMPSlider.value = unit.currentMP;
        unitHpDisplayText.text = $"{unit.currentHP} / {unit.maxHP}";
        unitMaxHp = unit.maxHP;

        if (typeof(PlayerUnit).IsAssignableFrom(unit.GetType()))
        {
            var player = unit as PlayerUnit;
            unitXpSlider.maxValue = player.expToLevel;
            unitXpSlider.value = player.currentExp;
        }
        else
        {
            unitXpSlider = null;
        }
    }

    public void SetHP(float hp)
    {
        unitHPSlider.value = hp;
        unitHpDisplayText.text = $"{hp} / {unitMaxHp}";
    }

    public void SetMP(float mp)
    {
        unitMPSlider.value = mp;
        
    }

    public void SetXp(float xp)
    {
        unitXpSlider.value = xp;
    }
}

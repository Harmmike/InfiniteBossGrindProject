using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text unitNameText;
    public Text unitLevelText;

    public Slider unitHPSlider;
    public Slider unitMPSlider;

    public Text unitHpDisplayText;
    public Text unitMpDisplayText;

    private float unitMaxHp;
    private float unitMaxMp;

    public void UpdateHUD(Unit unit)
    {
        unitNameText.text = unit.unitName;
        unitLevelText.text = $"Lv. {unit.unitLevel}";
        unitHPSlider.maxValue = unit.maxHP;
        unitHPSlider.value = unit.currentHP;
        unitMPSlider.maxValue = unit.MaxMP;
        unitMPSlider.value = unit.CurrentMP;
        unitHpDisplayText.text = $"{unit.currentHP} / {unit.maxHP}";
        unitMpDisplayText.text = $"{unit.CurrentMP} / {unit.MaxMP}";
        unitMaxHp = unit.maxHP;
<<<<<<< Updated upstream
        unitMaxMp = unit.maxMP;
=======
        unitMaxMp = unit.MaxMP;

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
>>>>>>> Stashed changes
    }

    public void SetHP(float hp)
    {
        unitHPSlider.value = hp;
        unitHpDisplayText.text = $"{hp} / {unitMaxHp}";
    }

    public void SetMP(float mp)
    {
        unitMPSlider.value = mp;
        unitMpDisplayText.text = $"{mp} / {unitMaxMp}";
    }
}

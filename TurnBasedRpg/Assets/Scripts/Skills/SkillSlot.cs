using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public event EventHandler<Skill> OnSkillClicked;

    public Image icon;

    public Skill skill;

    public GameObject mouseOverPanel;

    public void OnButtonClick()
    {
        OnSkillClicked?.Invoke(this, skill);
    }

    private void OnMouseOver()
    {
        mouseOverPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        mouseOverPanel.SetActive(false);
    }
}

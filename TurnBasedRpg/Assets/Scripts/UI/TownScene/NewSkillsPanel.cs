using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NewSkillsPanel : MonoBehaviour
{
    PlayerDataUnit player = PlayerDataTransfer.LoadPlayerData();

    public GameObject availableSkillsList;

    public Text availableSkillPointsText;

    public GameObject skillSlotPrefab;

    public SkillIcon[] skillIcons;

    public void UpdateUI()
    {
        availableSkillPointsText.text = $"Skill Points: {player.availableSkillPoints}";

        var skillSlots = availableSkillsList.GetComponentsInChildren<SkillSlot>();

        foreach (var skill in skillSlots)
        {
            Destroy(skill.gameObject);
        }

        foreach (var skill in SkillFactory.GameSkills)
        {
            SkillSlot skillSlot = Instantiate(skillSlotPrefab, availableSkillsList.transform).GetComponent<SkillSlot>();

            //Set icon
            foreach (var skillIcon in skillIcons)
            {
                if(skillIcon.skillID == skill.SkillID)
                {
                    skillSlot.icon = skillIcon.GetComponent<Image>();
                    skillSlot.GetComponentInChildren<Image>().sprite = skillSlot.icon.sprite;
                }
            }

            skillSlot.GetComponent<Button>().interactable = true;
            skillSlot.OnSkillClicked += NewSkillClicked;

            skillSlot.skill = skill;
            Skill newSkill = skillSlot.skill;

            newSkill.SkillID = skill.SkillID;
            newSkill.SkillName = skill.SkillName;
            newSkill.SkillType = skill.SkillType;
            newSkill.DamageModifier = skill.DamageModifier;
            newSkill.EnergyCost = skill.EnergyCost;
        }
    }

    private void NewSkillClicked(object sender, Skill skill)
    {
        if(player.availableSkillPoints < 1)
        {
            return;
        }

        if (player.knownSkills.Contains(skill))
        {
            //identify skill is already known
            return;
        }
        else
        {
            player.knownSkills.Add(skill);

            player.availableSkillPoints--;

            UpdateUI();
        }
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}

using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class AvailableSkills_UI : MonoBehaviour
{
    PlayerDataUnit player = PlayerDataTransfer.LoadPlayerData();

    public GameObject availableSkillsList;
    public GameObject equippedSkillsList;

    public GameObject skillPrefab;
    public SkillIcon[] skillIcons;

    public List<SkillSlot> playerEquippedSkills = new List<SkillSlot>();

    public void UpdateUI()
    {
        SetKnownSkills();
        SetEquippedSkills();
    }

    private void SetKnownSkills()
    {
        foreach (var skill in player.knownSkills)
        {
            SkillSlot skillSlot = Instantiate(skillPrefab, availableSkillsList.transform).GetComponent<SkillSlot>();

            //Set icon
            foreach (var skillIcon in skillIcons)
            {
                if (skillIcon.skillID == skill.SkillID)
                {
                    skillSlot.icon = skillIcon.GetComponent<Image>();
                    skillSlot.GetComponentInChildren<Image>().sprite = skillSlot.icon.sprite;
                }
            }

            skillSlot.GetComponent<Button>().interactable = true;
            skillSlot.OnSkillClicked += KnownSkillClicked;

            skillSlot.skill = skill;
            Skill newSkill = skillSlot.skill;

            newSkill.SkillID = skill.SkillID;
            newSkill.SkillName = skill.SkillName;
            newSkill.SkillType = skill.SkillType;
            newSkill.DamageModifier = skill.DamageModifier;
            newSkill.EnergyCost = skill.EnergyCost;
        }
    }

    private void SetEquippedSkills()
    {
        foreach (var skill in player.equippedSkills)
        {
            SkillSlot skillSlot = Instantiate(skillPrefab, equippedSkillsList.transform).GetComponent<SkillSlot>();

            //Set icon
            foreach (var skillIcon in skillIcons)
            {
                if (skillIcon.skillID == skill.SkillID)
                {
                    skillSlot.icon = skillIcon.GetComponent<Image>();
                    skillSlot.GetComponentInChildren<Image>().sprite = skillSlot.icon.sprite;
                }
            }

            skillSlot.skill = skill;
            Skill newSkill = skillSlot.skill;

            newSkill.SkillID = skill.SkillID;
            newSkill.SkillName = skill.SkillName;
            newSkill.SkillType = skill.SkillType;
            newSkill.DamageModifier = skill.DamageModifier;
            newSkill.EnergyCost = skill.EnergyCost;

            playerEquippedSkills.Add(skillSlot);
        }
    }

    public void ClosePanel()
    {
        PlayerDataTransfer.SavePlayerData(player);

        this.gameObject.SetActive(false);

        int availableSkillCount = player.knownSkills.Count;
        int equippedSkillCount = player.equippedSkills.Count;

        if (availableSkillCount != 0)
        {
            var skillSlots = availableSkillsList.GetComponentsInChildren<SkillSlot>();

            foreach (var skill in skillSlots)
            {
                Destroy(skill.gameObject);
            }
        }

        if(equippedSkillCount != 0)
        {
            var skillSlots = equippedSkillsList.GetComponentsInChildren<SkillSlot>();

            foreach (var skill in skillSlots)
            {
                Destroy(skill.gameObject);
            }
        }

        TownManager townManager = FindObjectOfType<TownManager>();
        townManager.megaBossButton.gameObject.SetActive(true);
        townManager.normalBossButton.gameObject.SetActive(true);
    }

    public void ClearSkills_Button()
    {
        int skillCount = player.equippedSkills.Count;

        if(skillCount == 0)
        {
            return;
        }
        else
        {
            var skillSlots = equippedSkillsList.GetComponentsInChildren<SkillSlot>();

            foreach (var skill in skillSlots)
            {
                Destroy(skill.gameObject);
            }

            player.equippedSkills.Clear();

            SetEquippedSkills();
        }        
    }
    private void KnownSkillClicked(object sender, Skill skill)
    {
        if(player.equippedSkills.Count >= 3)
        {
            //set up a popup to indicate this
            return;
        }

        if (player.equippedSkills.Contains(skill))
        {
            //identify skill is already equipped
            return;
        }
        else
        {
            SkillSlot skillSlot = Instantiate(skillPrefab, equippedSkillsList.transform).GetComponent<SkillSlot>();

            //Set icon
            foreach (var skillIcon in skillIcons)
            {
                if (skillIcon.skillID == skill.SkillID)
                {
                    skillSlot.icon = skillIcon.GetComponent<Image>();
                    skillSlot.GetComponentInChildren<Image>().sprite = skillSlot.icon.sprite;
                }
            }

            skillSlot.skill = skill;
            Skill newSkill = skillSlot.skill;

            newSkill.SkillID = skill.SkillID;
            newSkill.SkillName = skill.SkillName;
            newSkill.SkillType = skill.SkillType;
            newSkill.DamageModifier = skill.DamageModifier;
            newSkill.EnergyCost = skill.EnergyCost;

            player.equippedSkills.Add(newSkill);

            playerEquippedSkills.Add(skillSlot);
        }
    }
}

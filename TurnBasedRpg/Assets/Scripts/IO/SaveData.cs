using System.Collections.Generic;

namespace Assets.Scripts.IO
{
    [System.Serializable]
    public class SaveData
    {
        public string SavedName;
        public int SavedLevel;
        public int SavedPower;
        public int SavedSpeed;
        public float SavedHP;
        public float SavedMP;
        public int SavedGold;
        public float SavedCurrentXP;
        public float SavedXpToLvl;
        public int SavedStatPoints;
        public int SavedSkillPoints;
        public List<Skill> SavedKnownSkills = new List<Skill>();
        public List<Skill> SavedEquippedSkills = new List<Skill>();
    }
}

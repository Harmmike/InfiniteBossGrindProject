using System.Collections.Generic;

namespace Assets.Scripts.IO
{
    [System.Serializable]
    public class SaveData
    {
        public string SavedName;
        public int SavedLevel;
        public int SavedPower;
        public int SavedDex;
        public float SavedHP;
        public int SavedIntelligence;
        public int SavedGold;
        public int SavedCurrentXP;
        public int SavedXpToLvl;
        public int SavedStatPoints;
        public int SavedSkillPoints;
        public List<Skill> SavedKnownSkills = new List<Skill>();
        public List<Skill> SavedEquippedSkills = new List<Skill>();
    }
}

using UnityEngine;
using UnityEngine.Assertions.Must;

public static class BattleLogicManager
{
    public static bool PerformNormalAttack(Unit attacker, Unit target)
    {
        attacker.GainEnergy(2);
        Debug.Log("Normal Attack");
        return target.TakePhysicalDamage(attacker.unitPower * 1.5f);
    }

    public static bool AttackWithSkill(Unit attacker, Unit target, Skill skillUsed)
    {
        if(attacker.CurrentMP >= skillUsed.EnergyCost)
        {
            attacker.LoseEnergy(skillUsed.EnergyCost);

            //apply skill effects
            //TODO: Add checks for magic/true damage
            switch (skillUsed.SkillEffectOne)
            {
                default:
                    break;

                case EffectOfSkill.ATTACKHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.TakePhysicalDamage(CalculateSkillDamage(attacker.unitPower, attacker.unitDex, skillUsed));
                    }
                    if(skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        target.TakeMagicDamage(CalculateSkillDamage(attacker.unitIntelligence, attacker.unitDex, skillUsed));
                    }
                    break;

                case EffectOfSkill.ATTACKMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.LoseEnergy(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if(skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        target.LoseEnergy(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;

                case EffectOfSkill.GAINHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainHitPoints(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if(skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        attacker.GainHitPoints(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;

                case EffectOfSkill.GAINMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainEnergy(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if(skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        attacker.GainEnergy(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;
            }

            switch (skillUsed.SkillEffectTwo)
            {
                default:
                    break;

                #region Old system
                //case EffectOfSkill.ATTACKHP:
                //    if (skillUsed.SkillType == TypeOfSkill.Physical)
                //    {
                //        target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                //    }
                //    break;

                //case EffectOfSkill.ATTACKMP:
                //    if (skillUsed.SkillType == TypeOfSkill.Physical)
                //    {
                //        target.LoseEnergy(skillUsed.SkillEffectModifier);
                //    }
                //    break;

                //case EffectOfSkill.GAINHP:
                //    if (skillUsed.SkillType == TypeOfSkill.Physical)
                //    {
                //        attacker.GainHitPoints(skillUsed.SkillEffectModifier * attacker.unitPower);
                //    }
                //    break;

                //case EffectOfSkill.GAINMP:
                //    if (skillUsed.SkillType == TypeOfSkill.Physical)
                //    {
                //        attacker.GainEnergy(skillUsed.SkillEffectModifier * attacker.unitPower);
                //    }
                //    break;
                #endregion

                case EffectOfSkill.ATTACKHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.TakePhysicalDamage(CalculateSkillDamage(attacker.unitPower, attacker.unitDex, skillUsed));
                    }
                    if (skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        target.TakeMagicDamage(CalculateSkillDamage(attacker.unitIntelligence, attacker.unitDex, skillUsed));
                    }
                    break;

                case EffectOfSkill.ATTACKMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.LoseEnergy(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if (skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        target.LoseEnergy(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;

                case EffectOfSkill.GAINHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainHitPoints(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if (skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        attacker.GainHitPoints(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;

                case EffectOfSkill.GAINMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainEnergy(CalculateSkillEffect(attacker.unitPower, skillUsed));
                    }
                    if (skillUsed.SkillType == TypeOfSkill.Magic)
                    {
                        attacker.GainEnergy(CalculateSkillEffect(attacker.unitIntelligence, skillUsed));
                    }
                    break;
            }
        }

        Debug.Log(skillUsed.SkillName);
        return target.IsDead();
    }

    private static float CalculateSkillDamage(int attackPower, int dex, Skill skill = null)
    {
        float damage = 0;
        int critModifier = 2;

        if (skill == null)
        {
            damage = attackPower * 1.5f;
        }
        else
        {
            damage = attackPower * skill.DamageModifier;
        }

        if (DetermineIfCriticalHit(dex))
        {
            damage *= critModifier;
        }

        return damage;
    }

    private static float CalculateSkillEffect(int intelligence, Skill skill)
    {
        return intelligence * skill.SkillEffectModifier;
    }

    private static bool DetermineIfCriticalHit(int dex)
    {
        bool isCrit = false;
        float baseCritPercentage = 0.1f;
        float adjustedCritPercentage = (dex * 0.02f) + baseCritPercentage;

        if(adjustedCritPercentage >= 0.85)
        {
            adjustedCritPercentage = 0.85f;
        }

        var critRoll = Random.Range(0, 1f);

        if(critRoll <= adjustedCritPercentage)
        {
            //possibly set up event system for special
            //crit effects like large or colored numbers
            Debug.Log("Is a crit");
            isCrit = true;
        }
        else
        {
            isCrit = false;
        }
        return isCrit;
    }
}

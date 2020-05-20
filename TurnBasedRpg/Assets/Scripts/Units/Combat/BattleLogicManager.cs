using UnityEngine;

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
        if(attacker.currentMP >= skillUsed.EnergyCost)
        {
            attacker.LoseEnergy(skillUsed.EnergyCost);

            //apply skill effects
            //TODO: Add checks for magic/true damage
            switch (skillUsed.SkillEffectOne)
            {
                case EffectOfSkill.ATTACKHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                        target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.ATTACKMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.LoseEnergy(skillUsed.SkillEffectModifier * attacker.unitPower);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.GAINHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainHitPoints(skillUsed.SkillEffectModifier * attacker.unitPower);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.GAINMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainEnergy(skillUsed.SkillEffectModifier * attacker.unitPower);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;
            }

            switch (skillUsed.SkillEffectTwo)
            {
                default:
                    break;

                case EffectOfSkill.ATTACKHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                        target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.ATTACKMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        target.LoseEnergy(skillUsed.SkillEffectModifier);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.GAINHP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainHitPoints(skillUsed.SkillEffectModifier * attacker.unitPower);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;

                case EffectOfSkill.GAINMP:
                    if (skillUsed.SkillType == TypeOfSkill.Physical)
                    {
                        attacker.GainEnergy(skillUsed.SkillEffectModifier * attacker.unitPower);
                        //isDead = target.TakePhysicalDamage(skillUsed.DamageModifier * attacker.unitPower);
                    }
                    break;
            }
        }

        Debug.Log(skillUsed.SkillName);
        return target.IsDead();
    }
}

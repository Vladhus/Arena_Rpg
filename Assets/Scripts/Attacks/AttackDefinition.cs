using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleMagicMoba
{

    [CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
    public class AttackDefinition : ScriptableObject
    {
        public float Cooldown;

        public float Range;
        public float minDamage;
        public float maxDamage;
        public float criticalMultiplier;
        public float criticalChance;

        public Attack CreateAttack(CharacterStats wielderStats, CharacterStats defenderStats)
        {
            float coreDamage = wielderStats.characterDefinition.baseDamage;
            coreDamage += Random.Range(minDamage, maxDamage);

            bool isCritical = Random.value < criticalChance;
            if (isCritical)
                coreDamage *= criticalMultiplier;

            if (defenderStats != null)
                coreDamage -= defenderStats.GetResistance();

            return new Attack((int)coreDamage, isCritical);
        }

        public Attack CreateSpellAttack(CharacterStats CasterStats, CharacterStats defenderStats)
        {
            float coreSpellDamage = CasterStats.characterDefinition.currentMagicDamage;
            coreSpellDamage += Random.Range(minDamage, maxDamage);

            bool isCritical = Random.value < criticalChance;
            if (isCritical)
                coreSpellDamage *= criticalMultiplier;

            if (defenderStats != null)
                coreSpellDamage -= defenderStats.GetSpellResistance();

            return new Attack((int)coreSpellDamage, isCritical);
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SingleMagicMoba
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterStats_SO characterDefinition_Template;
        public CharacterStats_SO characterDefinition;

        public GameObject characterWeaponSlot;
        #region Initializations
        private void Awake()
        {
            if (characterDefinition_Template != null)
                characterDefinition = Instantiate(characterDefinition_Template);
        }
        void Start()
        {
            if (characterDefinition.isHero)
            {
                characterDefinition.SetCharLevel(0);
            }
        }
        #endregion

        #region Stat Increasers
        public void ApplyHealth(int healthAmount)
        {
            characterDefinition.ApplyHealth(healthAmount);
        }

        public void ApplyMana(int manaAmount)
        {
            characterDefinition.ApplyMana(manaAmount);
        }

        public void GiveWealth(int wealthAmount)
        {
            characterDefinition.GiveWealth(wealthAmount);
        }


        #endregion

        #region Stat Reducers
        public void TakeDamage(int amount)
        {
            characterDefinition.TakeDamage(amount);
        }

        public void TakeMana(int amount)
        {
            characterDefinition.TakeMana(amount);
        }

        #endregion

        #region Reporters
        public int GetHealth()
        {
            return characterDefinition.currentHealth;
        }



        public int GetDamage()
        {
            return characterDefinition.currentDamage;
        }

        public float GetResistance()
        {
            return characterDefinition.currentResistance;
        }

        public float GetSpellResistance()
        {
            return characterDefinition.currentSpellResistance;
        }

        #endregion

        #region Stat Initializers
        public void SetInitialeEalth(int health)
        {
            characterDefinition.maxHealth = health;
            characterDefinition.currentHealth = health;
        }

        public void SetInitialResistance(float resistance)
        {
            characterDefinition.baseResistance = resistance;
            characterDefinition.currentResistance = resistance;
        }

        public void SetInitialSpellResistance(float spellResistance)
        {
            characterDefinition.baseSpellResistance = spellResistance;
            characterDefinition.currentSpellResistance = spellResistance;
        }

        public void SetInitalDamage(int damage)
        {
            characterDefinition.baseDamage = damage;
            characterDefinition.currentDamage = damage;
        }

        #endregion
    }


}
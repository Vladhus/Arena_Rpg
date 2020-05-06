using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SingleMagicMoba
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Stats objects")]
        public CharacterStats_SO characterDefinition_Template;
        public CharacterStats_SO characterDefinition;

        [Header("Inventory objects")]
        public CharacterInv inventory;

        [Header("Weapon objects")]
        public GameObject characterWeaponSlot;

        [Header("Spell menu")]
        public ItemPickUp[] Spells;

        private int Counter = 1;

        #region Initializations
        private void Awake()
        {
            //characterDefinition.InstanceResistance = 10f;
            //Instantiate current template
            if (characterDefinition_Template != null)
                characterDefinition = Instantiate(characterDefinition_Template);

            if (characterDefinition.isHero)
            {


                //Set the first spell from Spell menu
                SetInitialAbilitySpell(Spells[0]);
            }

        }
        void Start()
        {
            //Check to see if it`s a character or not 
            if (characterDefinition.isHero)
            {
                //if character set the LEVEL
                characterDefinition.SetCharLevel(0);

                characterDefinition.skillIsChangedByPickUpItem.AddListener(IHeadAboutItToo);
                // SetInitialAbilitySpell(Spells[0]);
            }
        }
        #endregion

        #region Stat Increasers

        public void IHeadAboutItToo(bool skillISchanged)
        {
            Spells[Spells.Length - 1] = characterDefinition.CharSkills.spellBuffer;
        }
        //Add health
        public void ApplyHealth(int healthAmount)
        {
            characterDefinition.ApplyHealth(healthAmount);
        }

        //Add mana
        public void ApplyMana(int manaAmount)
        {
            characterDefinition.ApplyMana(manaAmount);
        }



        //Add money


        public void GiveWealth(int wealthAmount) => characterDefinition.GiveWealth(wealthAmount);
        
        
        
       

        #endregion

        #region Stat Reducers

        //Reduce HP
        public void TakeDamage(int amount)
        {
            characterDefinition.TakeDamage(amount);
        }

        //Reduce mana 
        public void TakeMana(int amount)
        {
            characterDefinition.TakeMana(amount);
        }

        #endregion

        #region Reporters

        //Get main ability spell => CharacterStats_SO
        public ItemPickUp GetCurrentSkill()
        {
            return characterDefinition.CharSkills.spellBuffer;
        }


        //Get Ultimate Ability Spell => CharacterStats_SO
        public ItemPickUp GetUltimateSkill()
        {
            return characterDefinition.CharSkills.UltimateSpellBuffer;
        }

        //Get range weapon => CharacterStats_SO
        public ItemPickUp GetCurrentWeaponRange()
        {
            return characterDefinition.weaponRange;
        }

        //Get melee weapon => CharacterStats_SO
        public ItemPickUp GetCurrentWeaponMelee()
        {
            return characterDefinition.weaponMelee;
        }

        //Get head Armor => CharacterStats_SO
        public ItemPickUp GetCurrentHeadArmor()
        {
            return characterDefinition.headArmor;
        }

        //Get chest Armor => CharacterStats_SO
        public ItemPickUp GetCurrentChestArmor()
        {
            return characterDefinition.chestArmor;
        }

        //Get hands Armor=> CharacterStats_SO
        public ItemPickUp GetCurrentHandsArmor()
        {
            return characterDefinition.handArmor;
        }

        //Get legs Armor=> CharacterStats_SO
        public ItemPickUp GetCurrentLegsArmor()
        {
            return characterDefinition.legArmor;
        }

        //Get foot Armor => CharacterStats_SO
        public ItemPickUp GetCurrentFootArmor()
        {
            return characterDefinition.footArmor;
        }

        //Get current Health => CharacterStats_SO
        public int GetHealth()
        {
            return characterDefinition.currentHealth;
        }

        //Get current Mana => CharacterStats_SO
        public int GetMana()
        {
            return characterDefinition.currentMana;
        }

        //Get current damage => CharacterStats_SO
        public float GetDamage()
        {
            return characterDefinition.currentDamage;
        }

        //Get current spell damage => CharacterStats_SO
        public float GetMagicDamage()
        {
            return characterDefinition.currentMagicDamage;
        }

        //Get current phys Resistance => CharacterStats_SO
        public float GetResistance()
        {
            return characterDefinition.currentResistance;
        }

        //Get current spell Damage => CharacterStats_SO
        public float GetSpellResistance()
        {
            return characterDefinition.currentSpellResistance;
        }

        #endregion

        #region Stats Initializers

        //Set start Health => CharacterStats_SO
        public void SetInitialeEalth(int health)
        {
            characterDefinition.maxHealth = health;
            characterDefinition.currentHealth = health;
        }

        //Set start Damage 
        public void SetInitalDamage(int damage)
        {
            characterDefinition.baseDamage = damage;
            characterDefinition.currentDamage = damage;
        }

        //Set start magic Damage 
        public void SetInitalMagicDamage(int damage)
        {
            characterDefinition.baseMagicDamage = damage;
            characterDefinition.currentMagicDamage = damage;
        }

        //Set start phys Resistance => CharacterStats_SO
        public void SetInitialResistance(float resistance)
        {
            characterDefinition.baseResistance = resistance;
            characterDefinition.currentResistance = resistance;
        }

        //Set start magic Resistance => CharacterStats_SO
        public void SetInitialSpellResistance(float spellResistance)
        {
            characterDefinition.baseSpellResistance = spellResistance;
            characterDefinition.currentSpellResistance = spellResistance;
        }

        #endregion

        #region Spells Initializers
        //Set the first ability spell of the character CharacterStats_SO=>EquipSkill
        public void SetInitialAbilitySpell(ItemPickUp abilitySpell)
        {
            characterDefinition.EquipSkill(abilitySpell, inventory);
        }

        //Set the receiver to change current spell with the next spell  CharacterStats => SetNextAbilitySpell
        public void SetNextAbilityListener(bool trueForSet)
        {
            SetNextAbilitySpell(Spells);
        }

        //Function for spell change
        public void SetNextAbilitySpell(ItemPickUp[] abilitySpell)
        {

            //Check to see if Current Spell it`s the same as first spell in spellsBook(Spells)
            if (Counter >= abilitySpell.Length)
                Counter = 0;
            
            //if yes than change to next spell from spellsBook
            characterDefinition.EquipSkill(abilitySpell[Counter], inventory);
            
           
            ++Counter;
            
        }

        #endregion

        #region Change Skill Weapon Armor

        public void ChangeWeapon(ItemPickUp weaponPickUp)
        {
            if (!characterDefinition.UnEquipWeapon(weaponPickUp, inventory, characterWeaponSlot))
            {
                Debug.LogError("Nice sec");
                characterDefinition.EquipWeapon(weaponPickUp, inventory, characterWeaponSlot);
            }
        }

        public void ChangeArmor(ItemPickUp armorPickUp)
        {
            if (!characterDefinition.UnEquipArmor(armorPickUp,inventory))
            {
                characterDefinition.EquipArmor(armorPickUp, inventory);
            }
        }

        public void ChangeAbilitySkill(ItemPickUp skillToEquip)
        {
            characterDefinition.EquipSkill(skillToEquip, inventory);
        }
        #endregion

    }
}
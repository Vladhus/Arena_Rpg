using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace SingleMagicMoba
{

    [CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
    public class CharacterStats_SO : ScriptableObject
    {
        public Events.SkillIsChangedBySkillPickedUp skillIsChangedByPickUpItem = new Events.SkillIsChangedBySkillPickedUp();

        public bool isHero = false;

        #region ArmorSlots

        public ItemPickUp headArmor { get; private set; } //HEAD

        public ItemPickUp chestArmor { get; private set; } // CHEST

        public ItemPickUp handArmor { get; private set; } // HANDS

        public ItemPickUp legArmor { get; private set; } // LEGS

        public ItemPickUp footArmor { get; private set; } //FOOT

        #endregion

        #region WeaponSlots

        public ItemPickUp weaponMelee { get; private set; }

        public ItemPickUp weaponRange { get; private set; }

        #endregion

        #region Skill Slots

        [System.Serializable]
        public class CharSkill
        {
            public ItemPickUp spellBuffer; // ability spell
            public ItemPickUp UltimateSpellBuffer; //main spell
        }

        #endregion

        #region Level Stats
        [System.Serializable]
        public class CharLevel
        {
            public int maxHealth;
            public int maxMana;
            public int maxWealth;
            public int baseDamage;
            public int baseMagicDamage;
            public float baseResistance;
            public float baseSpellResistance;
            public int requiredXP;
        }

        #endregion

        #region Current_Hero_Stats

        public int maxHealth = 0; //MAX HP
        public int currentHealth = 0; //CURRENT HP

        public int maxWealth = 0; //MAX MONEY COUNT
        public int currentWealth = 0; // CURRENT MONEY COUNT

        public int maxMana = 0; //MAX MANA COUNT
        public int currentMana = 0; //CURRENT MANA COUNT 

        public float baseDamage = 0; //BASE PHYS DMG
        public float currentDamage = 0; //CURRENT PHYS DMG

        public float baseMagicDamage = 0; //BASE MAGIC DMG
        public float currentMagicDamage = 0; //CURRENT MAGIC DMG

        public float baseResistance = 0f; //BASE PHYS RESISTANCE
        public float currentResistance = 0f; //CURRENT PHYS RESISTANCE

        public float baseSpellResistance = 0f; //BASE MAGIC RESISTANCE
        public float currentSpellResistance = 0f; //CURRENT MAGIC RESISTANCE

        public int charExp = 0; //current XP counter
        public int charLevel = 0; //current LEVEL

        public CharLevel[] charLevels; //STATS LEVEL BUFFS ARRAY
        public CharSkill CharSkills; // CHAR SKILLS CLASS(CONTAINER)

        #endregion

        #region Equip Items

        public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInv charInv, GameObject weaponSlot)
        {
            
           // var weaponPickUpTypes = weaponPickUp.itemDefinition.ItemType.Where(item => item == ItemType.WEAPON);
            if ( weaponPickUp.itemDefinition.itemWeaponSubType == ItemWeaponSubType.Melee)
            {
                weaponMelee = weaponPickUp;
                foreach (var stat in weaponPickUp.itemDefinition.ItemArmorStatsSubType)
                {
                    switch (stat)
                    {
                        case ItemStatsToAddSubType.DAMAGE:
                            currentDamage += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MAGICDAMAGE:
                            currentMagicDamage += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.HP:
                            currentHealth += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MANA:
                            currentMana += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.RESSISTANCE:
                            currentResistance += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MAGICRESSISTANCE:
                            currentSpellResistance += weaponPickUp.itemDefinition.itemAmount;
                            break;
                    }
                }
            }
            else if ( weaponPickUp.itemDefinition.itemWeaponSubType == ItemWeaponSubType.Range)
            {
                weaponRange = weaponPickUp;
                foreach (var stat in weaponPickUp.itemDefinition.ItemArmorStatsSubType)
                {
                    switch (stat)
                    {
                        case ItemStatsToAddSubType.DAMAGE:
                            currentDamage += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MAGICDAMAGE:
                            currentMagicDamage += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.HP:

                            currentHealth += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MANA:

                            currentMana += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.RESSISTANCE:
                            currentResistance += weaponPickUp.itemDefinition.itemAmount;
                            break;

                        case ItemStatsToAddSubType.MAGICRESSISTANCE:
                            currentSpellResistance += weaponPickUp.itemDefinition.itemAmount;
                            break;
                    }
                }

            }
        }

        public void EquipSkill(ItemPickUp skillPickUp, CharacterInv charInv)
        {

           //.... var BuffItemsType = skillPickUp.itemDefinition.ItemType.Where(item => item == ItemType.BUFF);
            if ( skillPickUp.itemDefinition.ItemBuffScholl != ItemBuffScholl.ULTI)
            {
                CharSkills.spellBuffer = skillPickUp;
                if (skillIsChangedByPickUpItem != null)
                {
                    skillIsChangedByPickUpItem.Invoke(true);
                }
            }
            else if ( skillPickUp.itemDefinition.ItemBuffScholl == ItemBuffScholl.ULTI)
            {
                CharSkills.UltimateSpellBuffer = skillPickUp;
                if (skillIsChangedByPickUpItem != null)
                {
                    skillIsChangedByPickUpItem.Invoke(true);
                }
            }
        }
        public void EquipArmor(ItemPickUp armorPickUp, CharacterInv charInv)
        {
            //Check to find out itemSubType (HEAD,CHEST,LEGS, e.t.c)
            switch (armorPickUp.itemDefinition.ItemSubType)
            {
                case ItemArmorSubType.HEAD:

                    //Equip in HEAD slot
                    headArmor = armorPickUp;

                    //Loop to find out all itemArmorStatsSubType`s to add  
                    for (int i = 0; i < armorPickUp.itemDefinition.ItemArmorStatsSubType.Length; i++)
                    {
                        //Check to find out itemArmorStatsSubType to add
                        switch (armorPickUp.itemDefinition.ItemArmorStatsSubType[i])
                        {
                            //Stat to add - DAMAGE
                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage += armorPickUp.itemDefinition.itemAmount;
                                break;

                            //Stat to add - MAGIC DAMAGE
                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage += baseMagicDamage + armorPickUp.itemDefinition.itemAmount;
                                break;
                            //Stat to add - MAGICRESSISTANCE
                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            //Stat to add - RESSISTANCE
                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            //Stat to add - HP
                            case ItemStatsToAddSubType.HP:

                                currentHealth += armorPickUp.itemDefinition.itemAmount;
                                break;
                            //Stat to add - MANA
                            case ItemStatsToAddSubType.MANA:

                                currentMana += armorPickUp.itemDefinition.itemAmount;
                                break;

                        }
                    }
                    break;

                case ItemArmorSubType.CHEST:

                    //Equip in CHEST slot
                    chestArmor = armorPickUp;

                    for (int i = 0; i < armorPickUp.itemDefinition.ItemArmorStatsSubType.Length; i++)
                    {
                        switch (armorPickUp.itemDefinition.ItemArmorStatsSubType[i])
                        {
                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.HP:

                                currentHealth += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MANA:

                                currentMana += armorPickUp.itemDefinition.itemAmount;
                                break;
                        }
                    }
                    break;


                case ItemArmorSubType.BOOTS:

                    //Equip in BOOTS slot
                    footArmor = armorPickUp;

                    for (int i = 0; i < armorPickUp.itemDefinition.ItemArmorStatsSubType.Length; i++)
                    {
                        switch (armorPickUp.itemDefinition.ItemArmorStatsSubType[i])
                        {
                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.HP:

                                currentHealth += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MANA:

                                currentMana += armorPickUp.itemDefinition.itemAmount;
                                break;

                        }
                    }
                    break;

                case ItemArmorSubType.HANDS:

                    ////Equip in HANDS slot
                    handArmor = armorPickUp;

                    for (int i = 0; i < armorPickUp.itemDefinition.ItemArmorStatsSubType.Length; i++)
                    {
                        switch (armorPickUp.itemDefinition.ItemArmorStatsSubType[i])
                        {
                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.HP:

                                currentHealth += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MANA:

                                currentMana += armorPickUp.itemDefinition.itemAmount;
                                break;

                        }
                    }
                    break;

                case ItemArmorSubType.LEGS:

                    ////Equip in LEGS slot
                    legArmor = armorPickUp;

                    for (int i = 0; i < armorPickUp.itemDefinition.ItemArmorStatsSubType.Length; i++)
                    {
                        switch (armorPickUp.itemDefinition.ItemArmorStatsSubType[i])
                        {
                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.HP:

                                currentHealth += armorPickUp.itemDefinition.itemAmount;
                                break;
                            case ItemStatsToAddSubType.MANA:

                                currentMana += armorPickUp.itemDefinition.itemAmount;
                                break;

                        }
                    }
                    break;
            }
        }

        #endregion

        #region UnEquip Items
        public bool UnEquipWeapon(ItemPickUp weaponToUnEquip, CharacterInv characterInv, GameObject weaponSlot)
        {
            bool previousWeaponSame = false;

            if (weaponToUnEquip.itemDefinition.itemWeaponSubType == ItemWeaponSubType.Melee)
            {
                if (weaponMelee != null)
                {
                    if (weaponMelee == weaponToUnEquip)
                    {
                        previousWeaponSame = true;
                    }

                   // DestroyObject(weaponSlot.transform.GetChild(0).gameObject);
                    weaponMelee = null;

                    foreach (var stat in weaponToUnEquip.itemDefinition.ItemArmorStatsSubType)
                    {
                        switch (stat)
                        {
                            case ItemStatsToAddSubType.MANA:
                                currentMana -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.HP:
                                currentHealth -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;
                        }
                    }
                }
            }
            else if (weaponToUnEquip.itemDefinition.itemWeaponSubType == ItemWeaponSubType.Range)
            {
                if (weaponRange != null)
                {
                    if (weaponRange == weaponToUnEquip)
                    {
                        previousWeaponSame = true;
                    }

                    //DestroyObject(weaponSlot.transform.GetChild(0).gameObject);
                    weaponRange = null;
                    foreach (var stat in weaponToUnEquip.itemDefinition.ItemArmorStatsSubType)
                    {
                        switch (stat)
                        {
                            case ItemStatsToAddSubType.MANA:
                                currentMana -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.HP:
                                currentHealth -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.MAGICDAMAGE:
                                currentMagicDamage -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.DAMAGE:
                                currentDamage -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                currentSpellResistance -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;

                            case ItemStatsToAddSubType.RESSISTANCE:
                                currentResistance -= weaponToUnEquip.itemDefinition.itemAmount;
                                break;
                        }
                    }
                }

            }
            return previousWeaponSame;
        }
        public bool UnEquipArmor(ItemPickUp armorToUnEquip, CharacterInv characterInv)
        {
            bool previousArmorSame = false;
            switch (armorToUnEquip.itemDefinition.ItemSubType)
            {
                case ItemArmorSubType.HEAD:
                    if (headArmor != null)
                    {
                        if (headArmor == armorToUnEquip)
                        {
                            previousArmorSame = true;
                        }
                        foreach (var stat in armorToUnEquip.itemDefinition.ItemArmorStatsSubType)
                        {
                            switch (stat)
                            {
                                case ItemStatsToAddSubType.MAGICDAMAGE:
                                    currentMagicDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                    currentSpellResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.DAMAGE:
                                    currentDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.RESSISTANCE:
                                    currentResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MANA:
                                    currentMana -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.HP:
                                    currentHealth -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                            }
                        }
                        headArmor = null;
                    }
                    break;
                case ItemArmorSubType.CHEST:
                    if (chestArmor != null)
                    {
                        if (chestArmor == armorToUnEquip)
                        {
                            previousArmorSame = true;
                        }
                        foreach (var stat in armorToUnEquip.itemDefinition.ItemArmorStatsSubType)
                        {
                            switch (stat)
                            {
                                case ItemStatsToAddSubType.MAGICDAMAGE:
                                    currentMagicDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                    currentSpellResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.DAMAGE:
                                    currentDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.RESSISTANCE:
                                    currentResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MANA:
                                    currentMana -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.HP:
                                    currentHealth -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                            }
                        }
                        chestArmor = null;
                    }
                    break;
                case ItemArmorSubType.HANDS:
                    if (handArmor != null)
                    {
                        if (handArmor == armorToUnEquip)
                        {
                            previousArmorSame = true;
                        }
                        foreach (var stat in armorToUnEquip.itemDefinition.ItemArmorStatsSubType)
                        {
                            switch (stat)
                            {
                                case ItemStatsToAddSubType.MAGICDAMAGE:
                                    currentMagicDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                    currentSpellResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.DAMAGE:
                                    currentDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.RESSISTANCE:
                                    currentResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MANA:
                                    currentMana -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.HP:
                                    currentHealth -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                            }
                        }
                        handArmor = null;
                    }
                    break;
                case ItemArmorSubType.LEGS:
                    if (legArmor != null)
                    {
                        if (legArmor = armorToUnEquip)
                        {
                            previousArmorSame = true;
                        }
                        foreach (var stat in armorToUnEquip.itemDefinition.ItemArmorStatsSubType)
                        {
                            switch (stat)
                            {
                                case ItemStatsToAddSubType.MAGICDAMAGE:
                                    currentMagicDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                    currentSpellResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.DAMAGE:
                                    currentDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.RESSISTANCE:
                                    currentResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MANA:
                                    currentMana -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.HP:
                                    currentHealth -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                            }
                        }
                        legArmor = null;
                    }
                    break;
                case ItemArmorSubType.BOOTS:
                    if (footArmor != null)
                    {
                        if (footArmor == armorToUnEquip)
                        {
                            previousArmorSame = true;
                        }
                        foreach (var stat in armorToUnEquip.itemDefinition.ItemArmorStatsSubType)
                        {
                            switch (stat)
                            {
                                case ItemStatsToAddSubType.MAGICDAMAGE:
                                    currentMagicDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MAGICRESSISTANCE:
                                    currentSpellResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.DAMAGE:
                                    currentDamage -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.RESSISTANCE:
                                    currentResistance -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.MANA:
                                    currentMana -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                                case ItemStatsToAddSubType.HP:
                                    currentHealth -= armorToUnEquip.itemDefinition.itemAmount;
                                    break;
                            }
                        }

                        footArmor = null;
                    }
                    break;
            }

            return previousArmorSame;
        }

        #endregion

        #region Stat Increasers

        //Add health
        public void ApplyHealth(int healthAmount)
        {
            if ((currentHealth + healthAmount) > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += healthAmount;
            }
        }

        //Add mana
        public void ApplyMana(int manaAmount)
        {
            if ((currentMana + manaAmount) > maxMana)
            {
                currentMana = maxMana;
            }
            else
            {
                currentMana += manaAmount;
            }
        }

        //Add money
        public void GiveWealth(int wealthAmount)
        {
            if ((currentWealth + wealthAmount) > maxWealth)
            {
                currentWealth = maxWealth;
            }
            else
            {
                currentWealth += wealthAmount;
            }
        }

        public void AddToCurrentPhysDamage(float dmgAmount)
        {
            currentDamage += dmgAmount;
        }

        public void AddToCurrentMagicDamage(float dmgAmount)
        {
            currentMagicDamage += dmgAmount;
        }

        public void AddToCurrentRessistance(float resAmount)
        {
            currentResistance += resAmount;
        }

        private void AddToCurrentMagicRessistance(float resAmount)
        {
            currentSpellResistance += resAmount;
        }


        //public  float InstanceResistance
        //{
        //    get { return currentSpellResistance; }
        //    set
        //    {
        //         AddToCurrentMagicRessistance(value);

        //    }
        //}



        //Add XP points
        public void GiveXp(int xpAmount)
        {
            charExp += xpAmount; //Add xp to XP counter
            if (charLevel < charLevels.Length)
            {
                int levelTarget = charLevels[charLevel].requiredXP; // How much xp the player need to get next LEVEL

                if (charExp >= levelTarget) //if current xp > target xp count , than LEVEL UP
                {
                    SetCharLevel(charLevel);
                }
            }
        }

        #endregion

        #region Stat Reducers

        //Reduce HP 
        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                //Death();
            }
        }

        //Reduce mana
        public void TakeMana(int amount)
        {
            currentMana -= amount;

            if (currentMana < 0)
            {
                currentMana = 0;
            }
        }

        #endregion

        #region Death and LvlUP
        //private void Death()
        //{
        //    if (isHero)
        //        OnHeroDeath.Invoke();


        //}

        //Level UP
        public void SetCharLevel(int newLevel)
        {
            charLevel = newLevel + 1;

            if (newLevel > 0)
            {
                maxHealth = charLevels[newLevel].maxHealth;
                ApplyHealth(charLevels[newLevel].maxHealth - charLevels[newLevel - 1].maxHealth);

                maxMana = charLevels[newLevel].maxMana;
                ApplyMana(charLevels[newLevel].maxMana - charLevels[newLevel - 1].maxMana);

                maxWealth = charLevels[newLevel].maxWealth;
                GiveWealth(charLevels[newLevel].maxWealth - charLevels[newLevel - 1].maxWealth);


                baseDamage = charLevels[newLevel].baseDamage;
                AddToCurrentPhysDamage(charLevels[newLevel].baseDamage - charLevels[newLevel - 1].baseDamage);

                baseMagicDamage = charLevels[newLevel].baseMagicDamage;
                AddToCurrentMagicDamage(charLevels[newLevel].baseMagicDamage - charLevels[newLevel - 1].baseMagicDamage);


                baseResistance = charLevels[newLevel].baseResistance;
                AddToCurrentRessistance(charLevels[newLevel].baseResistance - charLevels[newLevel - 1].baseResistance);

                baseSpellResistance = charLevels[newLevel].baseSpellResistance;
                AddToCurrentMagicRessistance(charLevels[newLevel].baseSpellResistance - charLevels[newLevel - 1].baseSpellResistance);
            }
            else
            {
                maxHealth = charLevels[newLevel].maxHealth;
                currentHealth = maxHealth;

                maxMana = charLevels[newLevel].maxMana;
                currentMana = maxMana;

                maxWealth = charLevels[newLevel].maxWealth;
                currentWealth = maxWealth;

                baseDamage = charLevels[newLevel].baseDamage;
                currentDamage = baseDamage;

                baseMagicDamage = charLevels[newLevel].baseMagicDamage;
                currentMagicDamage = baseMagicDamage;

                baseResistance = charLevels[newLevel].baseResistance;
                currentResistance = baseResistance;

                baseSpellResistance = charLevels[newLevel].baseSpellResistance;
                currentSpellResistance = baseSpellResistance;
            }


            //if (charLevel > 1)
            //{
            //    //OnLevelUp.Invoke(charLevel);
            //}
        }
        #endregion
    }

}
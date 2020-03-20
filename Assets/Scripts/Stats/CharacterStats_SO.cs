using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
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

    public bool isHero = false;

    

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int maxMana = 0;
    public int currentMana = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public int baseMagicDamage = 0;
    public int currentMagicDamage = 0;

    public float baseResistance = 0f;
    public float currentResistance = 0f;

    public float baseSpellResistance = 0f;
    public float currentSpellResistance = 0f;

    public int charExp = 0;
    public int charLevel = 0;

    public CharLevel[] charLevels;



    #region Stat Increasers
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

    public void GiveXp(int xpAmount)
    {
        charExp += xpAmount;
        if (charLevel < charLevels.Length)
        {
            int levelTarget = charLevels[charLevel].requiredXP;
            if (charExp >= levelTarget)
            {
                SetCharLevel(charLevel);
            }
        }
    }

    #endregion
    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        

        if (currentHealth <= 0)
        {
            //Death();
        }
    }

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

    public void SetCharLevel(int newLevel)
    {
        charLevel = newLevel + 1;

        maxHealth = charLevels[newLevel].maxHealth;
        currentHealth = charLevels[newLevel].maxHealth;

        maxMana = charLevels[newLevel].maxMana;
        currentMana = charLevels[newLevel].maxMana;

        maxWealth = charLevels[newLevel].maxWealth;

        baseDamage = charLevels[newLevel].baseDamage;
        baseMagicDamage = charLevels[newLevel].baseMagicDamage;


        currentDamage = charLevels[newLevel].baseDamage;
        currentMagicDamage = charLevels[newLevel].baseMagicDamage;

        baseResistance = charLevels[newLevel].baseResistance;
        currentResistance = charLevels[newLevel].baseResistance;
        baseSpellResistance = charLevels[newLevel].baseSpellResistance;
        currentSpellResistance = charLevels[newLevel].baseSpellResistance;

        //if (charLevel > 1)
        //{
        //    //OnLevelUp.Invoke(charLevel);
        //}
    }
    #endregion
}

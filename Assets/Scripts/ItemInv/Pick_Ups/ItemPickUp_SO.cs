using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SingleMagicMoba
{
    public enum ItemType { HEALTH_POTION, MANA_POTION, WEALTH, WEAPON, ARMOR, BUFF, EMPTY } //ITEM TYPE

    public enum ItemArmorSubType { NONE, HEAD, CHEST, HANDS, LEGS, BOOTS } // ARMOR SUBTYPE

    public enum ItemWeaponSubType { Melee, Range, NONE } //WEAPON TYPE

    public enum ItemStatsToAddSubType { MAGICDAMAGE, MAGICRESSISTANCE, DAMAGE, RESSISTANCE, MANA, HP } //STATS TO ADD TYPE

    public enum ItemBuffScholl { DARK, FROST, FIRE, DESTRUCTION, ULTI, NONE } //SPELL TYPE




    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Dropped/Spawnable_item", order = 2)]
    public class ItemPickUp_SO : ScriptableObject
    {
        [SerializeField]
        private Image image;
        public string itemName = "New Item";

        public AttackDefinition attackDefinition1; //Link to Attack type field for Spell Book System

        public int itemAmount = 0; //ITEM STATS TO ADD AMOUNT 
        public int spawnChanceWeight = 0;

        public ItemType[] ItemType = { SingleMagicMoba.ItemType.BUFF}; //Initialize
        public ItemArmorSubType ItemSubType = ItemArmorSubType.NONE; //Initialize
        public ItemWeaponSubType itemWeaponSubType = ItemWeaponSubType.NONE; //Initialize
        public ItemBuffScholl ItemBuffScholl = ItemBuffScholl.NONE; //Initialize

        public Rigidbody itemSpawnObject = null;
        public Weapon weaponSlotObject = null;
        public Material itemMaterial = null;
        public Sprite itemIcon = null;

        public bool isEquiped = false;
        public bool isInteractable = false;
        public bool isStorable = false;
        public bool isUnique = false;
        public bool isIndestructable = false;
        public bool isQuestItem = false;
        public bool isStackable = false;
        public bool destroyOnUse = false;

        public float itemWeight = 0f;


        public ItemStatsToAddSubType[] ItemArmorStatsSubType; //Initialize (Item can have more than one stat to add)
    }
}
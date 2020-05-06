using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingleMagicMoba
{
    public class ItemPickUp : MonoBehaviour
    {
        public ItemPickUp_SO itemDefinition;

        public CharacterStats charStats;

        private CharacterInv charInventory;

        private GameObject foundStats;

        #region MyConstructors
        public ItemPickUp()
        {
            charInventory = CharacterInv.Instance;
        }
        #endregion


        private void Start()
        {
            
           
            if (charStats == null)
            {
                foundStats = GameObject.FindGameObjectWithTag("Player");
                charStats = foundStats.GetComponent<CharacterStats>();
            }
        }

        private void StoreItemInInventory()
        {
            charInventory.StoreItem(this);
        }

        public void UseItem()
        {
            foreach (var itemType in itemDefinition.ItemType)
            {
                switch (itemType)
                {
                    case ItemType.HEALTH_POTION:
                        charStats.ApplyHealth(itemDefinition.itemAmount);
                        break;
                    case ItemType.MANA_POTION:
                        charStats.ApplyMana(itemDefinition.itemAmount);
                        break;
                    case ItemType.WEALTH:
                        charStats.GiveWealth(itemDefinition.itemAmount);
                        break;
                    case ItemType.WEAPON:
                        charStats.ChangeWeapon(this);
                        break;
                    case ItemType.ARMOR:
                        charStats.ChangeArmor(this);
                        break;
                    case ItemType.BUFF:
                        Debug.LogError("NICEEEE");
                        charStats.ChangeAbilitySkill(this);
                        break;
                    case ItemType.EMPTY:
                        break;
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (itemDefinition.isStorable)
                {
                    StoreItemInInventory();
                }
                else
                {
                    this.UseItem();
                }
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingleMagicMoba;

public class CharacterInv : MonoBehaviour
{
    #region var declaration
    public static CharacterInv Instance;
    #endregion

    private void Start()
    {
        Instance = this;
    }


    public void StoreItem(ItemPickUp itemToStore)
    {

    }
}

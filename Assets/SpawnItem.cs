using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingleMagicMoba;

public class SpawnItem : MonoBehaviour
{
    public ItemPickUp_SO[] itemDefinitions;

    

    public Rigidbody itemSpawned { get; set; }
    public ItemPickUp itemType { get; set; }


    private void Start()
    {
        CreateSpawn();
    }

    public void CreateSpawn()
    {
        //Spawn with weighted possibilities
       

        
                itemSpawned = Instantiate(itemDefinitions[0].itemSpawnObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

               


                itemType = itemSpawned.GetComponent<ItemPickUp>();
                itemType.itemDefinition = itemDefinitions[0];
               
            
        
    }
}

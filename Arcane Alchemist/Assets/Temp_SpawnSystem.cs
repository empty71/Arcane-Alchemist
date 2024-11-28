using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_SpawnSystem : MonoBehaviour
{

    public GameObject objectToSpawn; // The prefab to spawn
    public Transform spawnLocation; // The location where the object will spawn
    public Item woodItem;           // The wood item reference from the inventory

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (CanSpawnObject())
            {
                Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
                Inventory.instance.RemoveItems(woodItem, 4); // Deduct 4 wood from inventory
                Debug.Log("Object spawned and 4 wood deducted.");
            }
            else
            {
                Debug.Log("Not enough wood to spawn the object.");
            }
        }
    }

    private bool CanSpawnObject()
    {
        // Check if the inventory has at least 4 wood and all necessary variables are assigned
        if (Inventory.instance == null || woodItem == null || objectToSpawn == null || spawnLocation == null)
        {
            Debug.LogError("Ensure all variables are assigned in the Inspector.");
            return false;
        }

        return Inventory.instance.ContainsItem(woodItem, 4);
    }
}


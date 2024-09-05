using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaceGameObjectOnTerrain : MonoBehaviour
{
    public Terrain terrain; // Terrain Component Reference
    public GameObject gameObjectToPlace; // GameObject to place on terrain
    public Vector3 positionOnTerrain; // Where to place the GameObject

    void Start()
    {
        if (terrain == null || gameObjectToPlace == null)
        {
            Debug.LogError("Terrain Component or GameObject is null");
            return;
        }

        // Retrieves the height at the specified point on the terrain
        float height = terrain.SampleHeight(positionOnTerrain) + terrain.transform.position.y;

        // Positions the GameObject on the Terrain surface at the retrieved height
        Vector3 newPosition = positionOnTerrain;
        newPosition.y = height;

        // Instantiates the GameObject at the new position
        Instantiate(gameObjectToPlace, newPosition, Quaternion.identity);
    }
}



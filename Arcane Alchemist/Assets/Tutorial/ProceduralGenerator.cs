
using UnityEngine;

using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfPrefabInstances = 200;
    public Vector3 generationAreaSize = new Vector3(100f, 1f, 100f);
    public Transform parentContainer;

    public float absoluteGroundLevel = 30f;
    public Terrain terrain; // Reference to the terrain

    void Start()
    {
        // If no parentContainer provided, instances will be generated as children of the generator.
        if (parentContainer == null)
        {
            parentContainer = transform.root;
        }

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < numberOfPrefabInstances; i++)
        {
            Vector3 randomPosition = GetRandomPositionInGenerationArea();
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Ensure the object is placed at the correct height on the terrain
            randomPosition.y = terrain.SampleHeight(randomPosition) + terrain.transform.position.y;

            Instantiate(prefab, randomPosition, randomRotation, parentContainer.transform);
        }
    }

    Vector3 GetRandomPositionInGenerationArea()
    {
        Vector3 randomPosition = new Vector3(
           Random.Range(-generationAreaSize.x / 2, generationAreaSize.x / 2),
           0f, // Initial y set to 0, but will be adjusted later based on terrain height
           Random.Range(-generationAreaSize.z / 2, generationAreaSize.z / 2)
       );

        return transform.position + randomPosition;
    }

    // Draw Gizmo to visualize the generation area
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, generationAreaSize);
    }
}

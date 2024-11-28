using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public int maxHits = 5; // Number of hits before the tree is destroyed
    private int currentHits;

    public GameObject logPrefab; // Assign your log prefab in the Inspector
    public int minLogs = 3;
    public int maxLogs = 5;

    public void TakeDamage()
    {
        currentHits++;
        if (currentHits >= maxHits)
        {
            DestroyTree();
        }
    }

    void DestroyTree()
    {
        // Spawn random logs
        int logCount = Random.Range(minLogs, maxLogs + 1);
        for (int i = 0; i < logCount; i++)
        {
            Instantiate(logPrefab, transform.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        }

        // Destroy the tree
        Destroy(gameObject);
    }
}

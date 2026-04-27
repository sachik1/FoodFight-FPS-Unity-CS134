using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public int targetsToSpawn = 10;
    public GameObject targetPrefab;

    void Start()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");

        // Destroy all existing targets first
        foreach (GameObject t in allTargets)
            Destroy(t);

        if (allTargets.Length < targetsToSpawn)
        {
            Debug.LogWarning("Not enough spawn points for requested target count!");
            targetsToSpawn = allTargets.Length;
        }

        // Shuffle
        List<GameObject> shuffled = new List<GameObject>(allTargets);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int rand = Random.Range(i, shuffled.Count);
            GameObject temp = shuffled[i];
            shuffled[i] = shuffled[rand];
            shuffled[rand] = temp;
        }

        // Spawn targets at first N positions
        for (int i = 0; i < targetsToSpawn; i++)
            Instantiate(targetPrefab, shuffled[i].transform.position, shuffled[i].transform.rotation);
    }
}
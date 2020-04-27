using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Menu Wave config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int numberOfEnemies = 7;
    [SerializeField] float moveSpeed;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float randomFactor = 0.3f;

    public GameObject GetEnemyPrefab() { return enemyPrefab;  }

    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetRandomFactor() { return randomFactor; }
}

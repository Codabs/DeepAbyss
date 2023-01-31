using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnList
{
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public SpawnPoint GetRandomSpawnPoint()
    {
        int rand = Random.Range(0, spawnPoints.Count);
        return spawnPoints[rand];
    }
}

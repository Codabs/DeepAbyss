using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Range(0,3)]
    public int step = 0;
    
    [Header("Generators")]
    public List<Generator> generators = new List<Generator>(4);
    public List<SpawnList> spawnLists = new List<SpawnList>();
    public GameObject generatorPrefab;
    [Space(5)]
    [Header("Monstre")]
    public SpawnMonstre spawnMonstre;
    public GameObject monstrePrefab;
    [Space(5)]
    [Header("Door")]
    public Door door;
 
    private void Start()
    {
        SpawnCurrentStep();
    }

    public void SpawnCurrentStep()
    {
        Spawn(spawnLists[step].GetRandomSpawnPoint());
        SpawnMonster(spawnMonstre);
    }

    public void Spawn(SpawnPoint spawnPoint)
    {
        if(generators[step] == null)
        {
            GameObject generator = Instantiate(generatorPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            generator.GetComponent<Generator>().door = door;
            generators[step] = generator.GetComponent<Generator>();
        }
       
    }
    public void ValidateStep()
    {
        step++;
        if (step >= generators.Count) 
        { 
            step = generators.Count - 1; 
        }
        
    }
    public void SpawnMonster(SpawnMonstre newSpawnMonstre)
    {
        if(step == 1)
        {
            GameObject monstre = Instantiate(monstrePrefab, newSpawnMonstre.transform.position, newSpawnMonstre.transform.rotation);
        }
    }
}

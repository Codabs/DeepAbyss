using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [Range(0,3)]
    public int step = 0;

    [Header("Generators")]
    public List<Generator> generators = new();
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
        /*if (step >= generators.Count) 
        { 
            step = generators.Count - 1; 
        }*/
        try
        {
            SpawnCurrentStep();
            SpawnMonster();
        }
        catch 
        {
            door.gameObject.SetActive(false);
        }

    }
    public void SpawnMonster()
    {
        if(step >= 1)
        {
            monstrePrefab.SetActive(true);
            FindObjectOfType<AudioManager>().PlaySound("SpawnMonster");
        }
    }
}

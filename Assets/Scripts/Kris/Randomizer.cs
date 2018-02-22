using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    //create an array of spawn points, assigned in inspector 
    public List<Transform> collectableSpawnPoints = new List<Transform>();

    Transform currentSpawnPoint;

    //create an array of collectables to choose from
    public GameObject[] items = new GameObject[3];

    // Use this for initialization
    void Start()
    {
        SpawnCollectables();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //selects spawn point
    public Transform GetCollectableSpawnPoint()
    {
        //randomly selects a point out of the array
        int index = Random.Range(0, collectableSpawnPoints.Count);

        currentSpawnPoint = collectableSpawnPoints[index];
        //returns the selected point
        return currentSpawnPoint;
  
    }

    //selects object to spawn
    public GameObject GetCollectable()
    {
        //selects one of the items from the array
        int index = Random.Range(0, items.Length);
        //returns the object selected
        return items[index];
    }

    // spawns the random object on the random point
    public GameObject SpawnCollectables()
    {
        //selects the spawn point and removes it from the table of choices
        Transform spawnPoint = GetCollectableSpawnPoint();
        collectableSpawnPoints.Remove(spawnPoint);

        //selects the object
        GameObject collectable = GetCollectable();
        //creates the object selected on the point selected
        GameObject c = Instantiate(collectable, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //spawns the object
        return c;
    }


    
}
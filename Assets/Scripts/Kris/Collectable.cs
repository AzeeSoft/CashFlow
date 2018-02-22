using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    // imports classes from randomizer
    
    public Randomizer Respawn;
    public GameManager Cash;

    public void Awake()
    {
       Cash = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Respawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Randomizer>();
    }







    // destroys itself upon pickup

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Cash.addCash(100);
            Respawn.SpawnCollectables();
            Debug.Log(Cash.curCash);
        }

    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController2 : MonoBehaviour
{

    public float speed = 5f;
    public Randomizer Respawn; 



    Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(hor, ver);
        movementVector *= speed;

        rb2d.velocity = movementVector;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            Respawn.SpawnCollectables();
            Debug.Log("Test");
        }

    }


}
    
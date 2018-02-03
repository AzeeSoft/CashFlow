using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float hor = Input.GetAxis("Horizontal");
	    float ver = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(hor, ver);
	    movementVector *= speed;

	    transform.position += movementVector;
	}
}

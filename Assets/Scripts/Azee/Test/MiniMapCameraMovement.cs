using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraMovement : MonoBehaviour
{

    GameObject playerGameObject;

	// Use this for initialization
	void Start () {
		playerGameObject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		focusOnPlayer();
	}

    void focusOnPlayer()
    {
        transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y, transform.position.z);
    }
}

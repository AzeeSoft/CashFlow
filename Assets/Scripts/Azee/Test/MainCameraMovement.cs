using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{

    public float followSpeed = 5f;

    GameObject playerGameObject;

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        focusOnPlayer();
    }

    void focusOnPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y, transform.position.z), Time.deltaTime * followSpeed);
    }
}

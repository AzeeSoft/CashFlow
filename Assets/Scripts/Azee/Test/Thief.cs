using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float movebackTolerance;
    public float stealRadius;
    public float stealTimeout;

    GameManager gameManager;
    Transform playerTransform;

    Rigidbody2D rb2d;
    Vector2 originalPosition;
    Transform moveTowardsTransform = null;

    float lastStealTime = 0;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        rb2d = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        moveTowardsTransform = null;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    checkPlayerInRange();


	    if (moveTowardsTransform)
	    {
	        Vector3 dir = moveTowardsTransform.position - transform.position;
	        dir = dir.normalized;

            rb2d.velocity = dir * moveSpeed;
        } else
	    {
	        float distFromOriginal = Math.Abs(Vector2.Distance(transform.position, originalPosition));

	        if (distFromOriginal > movebackTolerance)
	        {
                Vector2 dir = originalPosition - new Vector2(transform.position.x, transform.position.y);
                dir = dir.normalized;

                rb2d.velocity = dir * moveSpeed;
            } else if (distFromOriginal > 0)
            {
                transform.position = originalPosition;
                rb2d.velocity = Vector2.zero;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }

        if (Math.Abs(rb2d.velocity.magnitude) > 0)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,
                Vector2.SignedAngle(Vector2.up, rb2d.velocity.normalized));

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void checkPlayerInRange()
    {
        if (Vector2.Distance(originalPosition, playerTransform.position) < stealRadius && gameManager.curCash > 0)
        {
            if ((Time.time - lastStealTime) > stealTimeout)
            {
                moveTowardsTransform = playerTransform;
            }
        }
        else
        {
            moveTowardsTransform = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player") && ((Time.time - lastStealTime) > stealTimeout) && gameManager.curCash > 0)
        {
            gameManager.addCash(-100);
            moveTowardsTransform = null;
            lastStealTime = Time.time;
            gameManager.onCashStolen();
        }
    }
}

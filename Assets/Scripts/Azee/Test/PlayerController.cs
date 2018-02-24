using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;

    public Transform coinArrowTransform;

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

        if (Math.Abs(rb2d.velocity.magnitude) > 0)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,
                Vector2.SignedAngle(Vector2.up, rb2d.velocity.normalized));

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*rotationSpeed);
        }

        updateCoinArrow();
    }

    public void updateCoinArrow()
    {
        coinArrowTransform.position = transform.position;

        bool hideArrow = true;

        Collectable collectable = FindObjectOfType<Collectable>();
        if (collectable)
        {
            GameObject coinGameObject = collectable.gameObject;
            Transform coinTransform = coinGameObject.transform;

            if (!coinGameObject.GetComponent<Renderer>().isVisible)
            {
                Vector2 dir = coinTransform.position - transform.position;

                Quaternion targetRotation = Quaternion.Euler(coinArrowTransform.rotation.x,
                    coinArrowTransform.rotation.y,
                    Vector2.SignedAngle(Vector2.up, dir));

                coinArrowTransform.rotation = Quaternion.Lerp(coinArrowTransform.rotation, targetRotation,
                    Time.deltaTime*rotationSpeed);

                hideArrow = false;
            }
        }

        coinArrowTransform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = !hideArrow;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
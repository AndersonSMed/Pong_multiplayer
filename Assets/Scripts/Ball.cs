using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallBehavior {

    private bool moving = false;
    private Rigidbody2D rb;
    [SerializeField]
    private float initialSpeed = 150f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (!moving && networkObject.IsServer) {
            int side = (int)Mathf.Floor(Random.Range(0, 4));
            switch (side) {
                case 0:
                    rb.AddForce(Vector2.up * initialSpeed);
                    break;
                case 1:
                    rb.AddForce(Vector2.down * initialSpeed);
                    break;
                case 2:
                    rb.AddForce(Vector2.left * initialSpeed);
                    break;
                default:
                    rb.AddForce(Vector2.right * initialSpeed);
                    break;
            }
            networkObject.force = rb.velocity;
            moving = true;
        } else if (networkObject.IsServer) {
            networkObject.position = transform.position;
            networkObject.force = rb.velocity;
        }
    }

    private void LateUpdate() {
        rb.velocity = networkObject.force;
        transform.position = networkObject.position;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rbCollision != null) {
            rb.velocity += rbCollision.velocity / 4;
        }
        rb.velocity *= 1.1f;
        networkObject.force = rb.velocity;
    }
}

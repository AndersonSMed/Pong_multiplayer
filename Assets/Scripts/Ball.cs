using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
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
        if (GameManager.Instance.GameEnded() && networkObject.IsServer) {
            rb.velocity = Vector2.zero;
        }
        if (GameManager.Instance.GameStarted()) {
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
                moving = true;
            }
        }
        if (networkObject.IsServer) {
            networkObject.position = transform.position;
            return;
        }
        transform.position = networkObject.position;
    }

    public void Collided (Vector3 direction) {
        if (networkObject.IsServer) {
            rb.velocity = rb.velocity + (Vector2) direction * 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Wall") && networkObject.IsServer) {
            collision.gameObject.GetComponent<Wall>().KillPlayer();
        }
    }
}

using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerPlatformBehavior {

    [SerializeField]
    private float acceleration = 5f;
    [SerializeField]
    private uint player = 0;
    [SerializeField]
    private string axis;

    private Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        networkObject.alive = true;
        networkObject.position = transform.position;
    }

    void Update() {

        player = networkObject.player;

        if (networkObject.MyPlayerId != player) {
            transform.position = networkObject.position;
            gameObject.SetActive(networkObject.alive);
            return;
        }

        networkObject.position = transform.position;

        if (Input.GetAxis(axis) == 0f) {
            rb.velocity = Vector2.zero;
        }

    }

    private void FixedUpdate() {
        if (networkObject.MyPlayerId == player) {
            float newSpeed = Input.GetAxis(axis) * acceleration;
            rb.AddForce((axis == "Horizontal") ? Vector2.right * newSpeed : Vector2.up * newSpeed);
        }
    }

    public void SetId (uint ID) {
        networkObject.alive = true;
        networkObject.player = ID;
        Debug.Log(networkObject.player);
        Debug.Log(networkObject.player);
    }
}

using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
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

    private void Start() {
        if (networkObject.IsServer) {
            networkObject.position = transform.position;
        }
    }

    void Update() {

        player = networkObject.player;

        if (NetworkManager.Instance.Networker.Me.NetworkId != player) {
            transform.position = networkObject.position;
            return;
        }
    }

    private void FixedUpdate() {
        if (NetworkManager.Instance.Networker.Me.NetworkId == player) {
            Vector3 direction = ((axis == "Horizontal") ? Vector3.right : Vector3.up) * Input.GetAxis(axis) * Time.deltaTime * acceleration;
            transform.position += direction;
            if (!networkObject.IsServer) {
                networkObject.SendRpc(RPC_MOVE, Receivers.All, transform.position, direction);
            } else {
                networkObject.position = transform.position;
                networkObject.direction = direction;
            }
        }
    }

    public void SetId (uint ID) {
        if (networkObject.IsServer) {
            networkObject.alive = true;
            networkObject.player = ID;
        }
    }

    public override void move(RpcArgs args) {
        if (networkObject.IsServer) {
            networkObject.position = args.GetNext<Vector3>();
            networkObject.direction = args.GetNext<Vector3>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ball") && networkObject.IsServer) {
            collision.gameObject.GetComponent<Ball>().Collided(networkObject.direction);
        }
    }
}

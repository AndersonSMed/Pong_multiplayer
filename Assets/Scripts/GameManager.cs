using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameManagerBehavior {

    private static GameManager instance;

    [SerializeField]
    private List<GameObject> platforms;
    // List containing all the players networkID
    private List<uint> playersList;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(instance);
        }
    }

    public static GameManager Instance {
        get {
            return instance;
        }
    }

    public override void LogPlayer(RpcArgs args) {
        if (networkObject.IsServer) {
            playersList.Add(args.GetNext<uint>());
            SetPlatforms();
        }
    }

    private void SetPlatforms() {
        int i = playersList.Count - 1;
        platforms[i].GetComponent<Player>().SetId(playersList[i]);
    }

    private void Start() {
        if (networkObject.IsServer) {
            playersList = new List<uint>();
            playersList.Add(NetworkManager.Instance.Networker.Me.NetworkId);
            SetPlatforms();
            networkObject.GameEnded = false;
        }
        uint networkID = NetworkManager.Instance.Networker.Me.NetworkId;
        networkObject.SendRpc(RPC_LOG_PLAYER, Receivers.All, networkID);
    }

    private void Update() {
        if (networkObject.IsServer) {
            networkObject.WaitingPlayers = NetworkManager.Instance.Networker.Players.Count < 4;
            if (!networkObject.WaitingPlayers && !networkObject.GameStarted) {
                if (Input.GetAxis("Jump") > 0) {
                    networkObject.GameStarted = true;
                }
            }
            if (networkObject.GameStarted) {
                int playersDead = 0;
                for (int i = 0; i < playersList.Count; i++) {
                    if (!platforms[i].GetComponent<Player>().isAlive()) {
                        playersDead++;
                    }
                }
                if (playersDead == playersList.Count - 1) {
                    networkObject.GameEnded = true;
                }
            }
        }
    }

    public bool GameStarted () {
        return networkObject.GameStarted;
    }

    public bool GameEnded() {
        return networkObject.GameEnded;
    }

    public bool WaitingPlayers () {
        return networkObject.WaitingPlayers;
    }
}

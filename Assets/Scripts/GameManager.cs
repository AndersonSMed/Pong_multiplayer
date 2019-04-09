using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameManagerBehavior {

    [SerializeField]
    private List<GameObject> platforms;

    private static GameManager instance;
    // List containing all the players networkID
    private List<uint> playersList;

    public static GameManager Instance {
        get {
            return instance;
        }
    }

    public override void LogPlayer(RpcArgs args) {
        uint network = args.GetNext<uint>();
        Debug.Log(network);
        playersList.Add(network);
        if (networkObject.IsServer) {
            SetPlatforms();
        }
    }

    private void SetPlatforms() {
        int i = playersList.Count - 1;
        platforms[i].GetComponent<Player>().SetId(playersList[i]);
    }

    private void Awake() {
        if (instance != this) {
            Destroy(instance);
        }
        if (instance == null) {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (networkObject.IsServer) {
            playersList = new List<uint>();
            playersList.Add(networkObject.MyPlayerId);
            SetPlatforms();
        } else {
            networkObject.SendRpc(RPC_LOG_PLAYER, Receivers.Server, networkObject.MyPlayerId);
        }
    }
}

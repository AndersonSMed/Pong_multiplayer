using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameManagerBehavior {

    [SerializeField]
    private List<GameObject> platforms;
    // List containing all the players networkID
    private List<uint> playersList;

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
        }
        uint networkID = NetworkManager.Instance.Networker.Me.NetworkId;
        networkObject.SendRpc(RPC_LOG_PLAYER, Receivers.All, networkID);
    }
}

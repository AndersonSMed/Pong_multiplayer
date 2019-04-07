using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
	void Start () {
        NetworkManager.Instance.InstantiatePlayerPlatform();
    }
}

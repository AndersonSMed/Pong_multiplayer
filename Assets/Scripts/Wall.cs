using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [SerializeField]
    private GameObject platform;
	
	public void KillPlayer() {
        platform.GetComponent<Player>().KillPlayer();
    }
}

using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : PlayerPlatformBehavior{

    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private GameObject platform;
	
	void Update () {
	
        if (!networkObject.IsOwner) {
            transform.position = networkObject.position;
            platform.SetActive(networkObject.alive);
            return;
        }

        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;

        // Scale the speed to normalize for processors
        translation *= speed * Time.deltaTime;

        // Move the object by the given translation
        transform.position += translation;

    }
}

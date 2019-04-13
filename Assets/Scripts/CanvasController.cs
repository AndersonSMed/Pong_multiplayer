using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private Text waitingText;

    void Update () {
        waitingText.gameObject.SetActive(!GameManager.Instance.GameStarted());
	}
}

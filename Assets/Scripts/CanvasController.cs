using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private Text waitingText;
    [SerializeField]
    private Text waitingInputText;

    void Update () {
        waitingText.gameObject.SetActive(GameManager.Instance.WaitingPlayers());
        waitingInputText.gameObject.SetActive(!GameManager.Instance.WaitingPlayers() && !GameManager.Instance.GameStarted());
    }
}

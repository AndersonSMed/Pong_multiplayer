using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private Text waitingText;
    [SerializeField]
    private Text waitingInputText;
    [SerializeField]
    private Text playerIdText;
    [SerializeField]
    private Text gameEndedText;

    void Update () {
        waitingText.gameObject.SetActive(GameManager.Instance.WaitingPlayers());
        waitingInputText.gameObject.SetActive(!GameManager.Instance.WaitingPlayers() && !GameManager.Instance.GameStarted());
        gameEndedText.gameObject.SetActive(GameManager.Instance.GameEnded());
        playerIdText.text = "Your Player ID: " + NetworkManager.Instance.Networker.Me.NetworkId;
    }
}

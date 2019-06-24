using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.PunBehaviour {
    public static PhotonManager instance;
    public Text status = null;

    void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start () {
        PhotonNetwork.ConnectUsingSettings("Tanks_v1.0");
        ShowMessage("Connecting...");
    }

    public override void OnConnectedToPhoton() {
        ShowMessage("Connected");
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {
        ShowMessage("Failed Connect");
    }

    public override void OnDisconnectedFromPhoton() {
        ShowMessage("Disconnected");
    }

    // Room
    public void JoinGameRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Kingdom", options, null);
        ShowMessage("Joining Room...");
    }

    public override void OnJoinedRoom()
    {
        ShowMessage("You are joined room!!");
        // if is Master Client, can create/init and load game scene
        if (PhotonNetwork.isMasterClient)
        {
            ShowMessage("You are Master Client!!");
            PhotonNetwork.LoadLevel("GameRoomScene");
        }
    }

    void ShowMessage(string msg)
    {
        status.text = msg;
        Debug.Log("Phonton:" + msg);
    }

    void sceneLoaded(int levelNumber)
    {
        // if not in Photon Room, may be network problem
        if (!PhotonNetwork.inRoom)
            return;
        ShowMessage("Game Room Loaded!!");
    }
}

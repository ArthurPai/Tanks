using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.PunBehaviour {
    public static PhotonManager instance;

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
    }

    public override void OnConnectedToPhoton() {
        Debug.Log("Photon: Connected");
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause) {
        Debug.Log("Photon: Failed Connect");
    }

    public override void OnDisconnectedFromPhoton() {
        Debug.Log("Photon: Disconnected");
    }

    // Room
    public void JoinGameRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Kingdom", options, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You are joined room!!");
        // if is Master Client, can create/init and load game scene
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("You are Master Client!!");
        }
    }
}

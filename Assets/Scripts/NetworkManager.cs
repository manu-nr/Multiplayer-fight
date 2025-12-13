using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //private void OnConnectedToServer()
    //{
    //    Debug.Log("Connected to server");
    //    CreateOrJoinRoom();
    //}

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        CreateOrJoinRoom();
    }

    private void CreateOrJoinRoom()
    {
        Debug.Log("CreateOrJoinRoom");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnRoomCreated");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
  
}

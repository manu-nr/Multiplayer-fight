using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerSpawner _playerSpawner;

    private GameObject _player;
    private PlayerCombact _playerCombact;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnDestroy()
    {
    }



    public override void OnConnectedToMaster()
    {
        //Debug.Log("Connected to master");
        //PhotonNetwork.JoinRandomOrCreateRoom(); // temperory

    }
    public void CreateOrJoinRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom(); // main
    }

    public override void OnCreatedRoom()
    {
        //Debug.Log("OnRoomCreated");
    }

    public override void OnJoinedRoom()
    {
        object[] data = new object[] { GameManager.Instance.PlayerName};
        _player = PhotonNetwork.Instantiate("Player", _playerSpawner.GetPlayerPosition(), Quaternion.identity, 0, data);
        GameManager.Instance.SetPlayer(_player);
        _playerCombact = _player.GetComponent<PlayerCombact>();
    }
}

using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerSpawner _playerSpawner;

    private GameObject _player;
    private PlayerCombact _playerCombact;

    public GameObject MyPlayer => _player;

    public static event Action OnPlayerJoined;


    public static NetworkManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("[NRM] OnPlayerEnteredRoom: " + newPlayer.NickName);
        base.OnPlayerEnteredRoom(newPlayer);
        OnPlayerJoined?.Invoke();
    }

    

    public override void OnJoinedRoom()
    {
        OnPlayerJoined?.Invoke();
        object[] data = new object[] { GameManager.Instance.PlayerName};
        _player = PhotonNetwork.Instantiate("Player", _playerSpawner.GetPlayerPosition(PhotonNetwork.CurrentRoom.PlayerCount), Quaternion.identity, 0, data);
        GameManager.Instance.SetPlayer(_player);
        _playerCombact = _player.GetComponent<PlayerCombact>();

    }

    [PunRPC]
    public void SetPlayerPosition(Vector3 position)
    {
        _player.transform.position = position;
    }
    
}

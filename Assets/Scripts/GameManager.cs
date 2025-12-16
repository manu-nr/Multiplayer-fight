using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private JoinGameUI _joinGameUI;

    private string _playerName;
    private GameObject _myPlayer;
    private GameObject _opponentPlayer;

    public static GameManager Instance;
    public string PlayerName => _playerName;
    public NetworkManager NetworkManager => _networkManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {


    }

    private void Start()
    {
        JoinGameUI.JoinGameButtonClicked += HandleJoinGameButtonClicked;
    }

    private void OnDestroy()
    {
        JoinGameUI.JoinGameButtonClicked -= HandleJoinGameButtonClicked;
    }

    private void HandleJoinGameButtonClicked(string playerName)
    {
        _playerName = playerName;   
        DisableJoinGameUI();
        _networkManager.CreateOrJoinRoom();
    }

    private void DisableJoinGameUI()
    {
        _joinGameUI.gameObject.SetActive(false);
    }

    public void SetPlayer(GameObject player)
    {
        _myPlayer = player;
    }

    public void ReSpawnPlayer(GameObject player)
    {
        StartCoroutine(ReSpawnRoutine(player));
    }

    public IEnumerator ReSpawnRoutine(GameObject gameObject)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = Vector3.zero;
        gameObject.SetActive(true);
    }

}

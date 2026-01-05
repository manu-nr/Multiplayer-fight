using UnityEngine;
using Photon;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using Unity;
using UnityEngine.UI;
using System.Collections;
using System;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] private ChatUI _chatUI;

    private ChatClient _chatClient;
    private string _userName;
    private string _channel = "Global";

    public static ChatManager Instance;

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _chatClient = new ChatClient(this);

        JoinGameUI.JoinGameButtonClicked += HandleJoinGameButtonClicked;
    }

    private void Update()
    {
        _chatClient.Service();
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    SendMessages("Global", "TestFromUpdate");
        //}
    }

    private void OnDestroy()
    {
        JoinGameUI.JoinGameButtonClicked -= HandleJoinGameButtonClicked;
    }

    private void OnApplicationQuit()
    {
        _chatClient?.Disconnect();
    }
    #endregion

    #region Handlers
    private void HandleJoinGameButtonClicked(string playerName)
    {
        _userName = playerName;

        _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(_userName));
        StartCoroutine(SubscribeChannel());

        ToggleChatUI(true);
    }

    #endregion

    #region Private Methods

    private void ToggleChatUI(bool on)
    {
        if(on)
            _chatUI.gameObject.SetActive(true);
        else
            _chatUI.gameObject.SetActive(false);
    }

    private IEnumerator SubscribeChannel()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Subscribed");
        JoinChannel(_channel);
    }

    private void JoinChannel(string channelName)
    {
        _chatClient.Subscribe(channelName);
        _chatClient.PublishMessage(channelName, "Hello!");
    }

    #endregion

    #region Public Methods
    public void SendMessages(string message)
    {
        Debug.Log("Publishing message");
        _chatClient.PublishMessage(_channel, message);
    }

    public void DebugReturn(DebugLevel level, string message) { }
   

    public void OnChatStateChange(ChatState state) { }
   

    public void OnConnected() { Debug.Log("Connected to photon chat"); }


    public void OnDisconnected() { }


    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log("OnGetMessage");
        for(int i=0; i<messages.Length; i++)
        {
            _chatUI.DisplayMessage(senders[i], messages[i]);
        }
    }


    public void OnPrivateMessage(string sender, object message, string channelName) { }


    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }


    public void OnSubscribed(string[] channels, bool[] results) { }


    public void OnUnsubscribed(string[] channels) { }


    public void OnUserSubscribed(string channel, string user) { }


    public void OnUserUnsubscribed(string channel, string user) { }
    #endregion

}

using UnityEngine;
using Photon;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using Unity;
using UnityEngine.UI;
using System.Collections;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] private ChatUI _chatUI;

    private ChatClient _chatClient;
    private string _userName;


    private void Start()
    {
        _chatClient = new ChatClient(this);
        _userName = PhotonNetwork.NickName;

        _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(_userName));

        StartCoroutine(SubscribeChannel());
    }

    private void Update()
    {
        _chatClient.Service();
        if (Input.GetKeyDown(KeyCode.J))
        {
            SendMessages("Global", "TestFromUpdate");
        }
    }

    private void OnApplicationQuit()
    {
        _chatClient?.Disconnect();
    }

    private IEnumerator SubscribeChannel()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Subscribed");
        JoinChannel("Global");
    }

    private void JoinChannel(string channelName)
    {
        _chatClient.Subscribe(channelName);
        _chatClient.PublishMessage(channelName, "Hello!");
    }

    private void SendMessages(string reach, string message)
    {
        Debug.Log("Publishing message");
        _chatClient.PublishMessage(reach, message);
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
            Debug.Log($"Sender: {senders[i]}, Message: {messages[i]}");
        }
    }


    public void OnPrivateMessage(string sender, object message, string channelName) { }


    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }


    public void OnSubscribed(string[] channels, bool[] results) { }


    public void OnUnsubscribed(string[] channels) { }


    public void OnUserSubscribed(string channel, string user) { }


    public void OnUserUnsubscribed(string channel, string user) { }

}

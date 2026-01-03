using Photon.Chat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _messageTextField;
    [SerializeField] private Button _sendMessageButton;
    [SerializeField] private GameObject _messagePrefab;
    [SerializeField] private GameObject _messageContent;

    private bool _isTyping = false;

    #region Unity Methods
    private void Start()
    {
        _sendMessageButton.onClick.AddListener(HandleSendButtonClicked);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !_isTyping)
        {
            _messageTextField.Select();
            _messageTextField.ActivateInputField();
            _isTyping = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && _isTyping)
        {
            _messageTextField.DeactivateInputField();
            _isTyping = false;
        }
    }

    private void OnDestroy()
    {
        _sendMessageButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region Public Methods
    public void DisplayMessage(string sender, object message)
    {
        GameObject messageBox = Instantiate(_messagePrefab, _messageContent.transform);
        messageBox.GetComponent<TextMeshProUGUI>().text = $"{sender}: {message}";
    }
    #endregion

    #region Private Methods
    private void HandleSendButtonClicked() 
    {
        string message = _messageTextField.text;

        if(!string.IsNullOrEmpty(message))
        {
            ChatManager.Instance.SendMessages(message);
        }
    }
    #endregion
}

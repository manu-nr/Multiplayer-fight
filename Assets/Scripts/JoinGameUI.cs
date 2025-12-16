using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerName;
    [SerializeField] private Button _joinGameButton;

    public static event Action<string> JoinGameButtonClicked;

    void Start()
    {
        _joinGameButton.onClick.AddListener(HandleOnJoinGameButtonClicked);
        _playerName.onValueChanged.AddListener(SetButtonInteractable);
    }

    private void OnDestroy()
    {
        _joinGameButton.onClick.RemoveAllListeners();
        _playerName.onValueChanged.RemoveAllListeners();
    }

    private void SetButtonInteractable(string input)
    {
        _joinGameButton.interactable = !string.IsNullOrEmpty(input.Trim());
    }

    private void HandleOnJoinGameButtonClicked()
    {
        JoinGameButtonClicked?.Invoke(_playerName.text);
    }
}

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private GameObject _playerSettingsPrefab;
    [SerializeField] private GameObject _container;

    private TextMeshProUGUI _playerName;
    private Slider _voiceSlider;

    private void Start()
    {
        NetworkManager.OnPlayerJoined += UpdatePlayerSettings;
    }

    private void OnDestroy()
    {
        NetworkManager.OnPlayerJoined -= UpdatePlayerSettings;
    }

    private void UpdatePlayerSettings()
    {
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            Debug.Log("[NRM] Player entered: " + player.NickName);

            if (string.IsNullOrEmpty(player.NickName))
            {
                GameObject playerSettings = Instantiate(_playerSettingsPrefab, _container.transform);
                TextMeshProUGUI nameField = playerSettings.GetComponentInChildren<TextMeshProUGUI>();

                if (nameField != null)
                {
                    nameField.GetComponentInChildren<TextMeshProUGUI>().text = player.NickName;
                }
            }
            else
                StartCoroutine(RetryUpdatePlayer());
        }
    }

    private IEnumerator RetryUpdatePlayer()
    {
        yield return new WaitForSeconds(1000);
        Debug.Log("[NRM] Updating player again");
        UpdatePlayerSettings();
    }
}

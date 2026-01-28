using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private GameObject _playerSettingsPrefab;

    private TextMeshProUGUI _playerName;
    private Slider _voiceSlider;

    private void Start()
    {
        PlayerController.PlayerSpawned += UpdatePlayerSettings;
    }

    private void OnDestroy()
    {
        PlayerController.PlayerSpawned -= UpdatePlayerSettings;
    }

    private void UpdatePlayerSettings()
    {
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            //Implementation for instantiating player settings.
        }
    }
}

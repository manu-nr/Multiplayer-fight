using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Image _playerHealth;

    [SerializeField] private string _testName;
    [SerializeField] private int _testHealth;

    [SerializeField] private Canvas _playerUICanvas;

    private Camera _mainCamera;

    private void Start()
    {
        if(_playerUICanvas != null)
            _playerUICanvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SetPlayerName(_testName);
            SetPlayerHealth(_testHealth);   
        }
    }

    private void LateUpdate()
    {
        if (_mainCamera == null && CameraController.Instance != null)
            _mainCamera = CameraController.Instance.MainCamera;


        if (_mainCamera != null)
        {
            _playerUICanvas.transform.LookAt(_mainCamera.transform);
        }

    }

    public void SetPlayerName(string playerName)
    {
        _playerName.text = playerName;
    }

    public void SetPlayerHealth(int currentHealth)
    {
        _playerHealth.fillAmount = currentHealth/100f;
    }

    public void ResetPlayerUI()
    {
        SetPlayerName(GameManager.Instance.PlayerName);
        SetPlayerHealth(100);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Image _playerHealth;

    [SerializeField] private string _testName;
    [SerializeField] private int _testHealth;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SetPlayerName(_testName);
            SetPlayerHealth(_testHealth);   
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
}

using System;
using UnityEngine;

public class SwordAttackDetector : MonoBehaviour
{
    [SerializeField]private bool _startDetecting;
    private string _targetTag = "Player";

    public static event Action<bool> OnAttack;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering on trigger enter");
        if (other.CompareTag(_targetTag))
        {
            if (_startDetecting)
            {
                OnAttack?.Invoke(true);
                _startDetecting = false;
                Debug.Log("[NRM] Attacking opponent");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void SetDetect(bool detect)
    {
        _startDetecting = detect;
    }
}

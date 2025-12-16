using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    [SerializeField] private bool _isAttacking;
    [SerializeField] private int _playerHealth = 100;
    [SerializeField] private float _enemyRange = 2f;
    [SerializeField] private PlayerUIController _playerUIController;
    [SerializeField] private Transform _playerView;
    [SerializeField] private PhotonView _photonView;

    [SerializeField] private GameObject _enemyPlayer;

    private PlayerController _playerController;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if(_playerController == null)
            _playerController = GetComponent<PlayerController>();
    }

    #region Private Methods
    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        if (CheckForEnemy())
        {
            _enemyPlayer.GetComponent<PlayerCombact>().GotAttackedByEnemy(true);
        }

    }

    private bool CheckForEnemy()
    {
        Ray ray = new Ray(_playerView.position, _playerView.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, _enemyRange))
        {
            if(hit.collider.CompareTag("Player"))
            {
                _enemyPlayer = hit.collider.gameObject;
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Public Methods
    public void Attack(bool attack)
    {
        StartCoroutine(AttackRoutine());
    }

    public void GotAttacked()
    {
        _playerHealth -= 20;
        _playerUIController.SetPlayerHealth(_playerHealth);
        if(_playerHealth == 0)
        {
            if(_playerController != null)
            {
                _playerController.RespawnPlayer();
            }
        }
    }

    public void GotAttackedByEnemy(bool attacked)
    {
        _photonView.RPC("PlayerGotAttacked", RpcTarget.All, attacked);
    }

    [PunRPC]
    public void PlayerGotAttacked(bool gotAttacked)
    {
        GotAttacked();
    }

    #endregion
}

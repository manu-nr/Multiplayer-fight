using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviourPun
{

    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 0.05f;
    [SerializeField] private float _attackCooldownTime = 2.5f;

    [SerializeField] private PlayerCombact _playerCombact;
    [SerializeField] private PlayerUIController _playerUIController;


    private CharacterController _characterController;
    private Animator _animator;
    private bool _isAttacking;

    #region Unity Methods
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        if(photonView.InstantiationData != null)
        {
            string playerName = (string) photonView.InstantiationData[0];
            _playerUIController.SetPlayerName(playerName);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            HandlePlayerMovement();
            HandlePlayerAttack();
        }
    }
    #endregion

    #region Private Methods
    private void HandlePlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!_isAttacking)
            {
                Attack();
            }
        }
    }    
    private void HandlePlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        bool isMoving = movement != Vector3.zero ? true : false;

        if(isMoving && !_isAttacking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
            _characterController.Move(movement.normalized * _movementSpeed * Time.deltaTime);

            SetRunAnimation();
        }
        else
        {
            if(_animator.GetBool("IsRunning") && !_isAttacking)
                SetIdleAnimation();
        }
    }

    private void Attack()
    {
        _isAttacking = true;
        _playerCombact.Attack(_isAttacking);
        SetAttackAnimation();
        StartCoroutine(SetIsAttacking());
    }

    private void SetAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    private void SetRunAnimation()
    {
        _animator.SetBool("IsRunning", true);
    }

    private void SetIdleAnimation()
    {
        _animator.SetBool("IsRunning", false);
    }
    #endregion

    #region Public Methods
    public void RespawnPlayer()
    {
        //GameManager.Instance.RespawnPlayer();
    }

    #endregion

    #region Coroutines
    private IEnumerator SetIsAttacking()
    {
        yield return new WaitForSeconds(_attackCooldownTime);
        _isAttacking = false;
    }

    #endregion
}

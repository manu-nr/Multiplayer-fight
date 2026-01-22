using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    public static PooledObjects Instance;

    [Header("Chat Message")]

    [SerializeField] private GameObject _chatMessageGameObject;
    [SerializeField] private Transform _messageContentTransform;
    [SerializeField] private int _maxChatMessages = 5;
    [SerializeField] private List<GameObject> _chatMessageGameObjectList = new List<GameObject>();

    private int _currentMessageGameObjectIndex = 0;

    #region Unity Methods

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        SpawnChatMessageGameObjects();
    }
    #endregion

    #region Private Methods
    private void SpawnChatMessageGameObjects()
    {
        for(int i=0; i<_maxChatMessages; i++)
        {
            GameObject chatMessage = Instantiate(_chatMessageGameObject, _messageContentTransform);
            chatMessage.SetActive(false);
            _chatMessageGameObjectList.Add(chatMessage);
        }
    }
    #endregion

    #region Public Methods

    public GameObject GetChatMessage()
    {
        if(_currentMessageGameObjectIndex >= _maxChatMessages)
            _currentMessageGameObjectIndex = 0;

        return _chatMessageGameObjectList[_currentMessageGameObjectIndex++];
    }


    #endregion
}

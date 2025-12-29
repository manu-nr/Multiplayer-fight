using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();
    private int _spawnCount = 0;

    public Vector3 GetPlayerPosition()
    {
        return _spawnPositions[_spawnCount].position;
    }
}

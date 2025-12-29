using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();
    private int _spawnCount = 0;

    public Vector3 GetPlayerPosition(int playerCount)
    {
        //Debug.Log("[NRM] Player spawn position: " + _spawnCount);
        return _spawnPositions[playerCount - 1].position;
    }
}

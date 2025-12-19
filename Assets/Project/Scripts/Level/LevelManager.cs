using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField]private LevelData _currentLevelData;
    public LevelData CurrentLevelData => _currentLevelData;

	[Header("Tile")]
    [SerializeField]private GameObject _tilePrefab;
    [SerializeField]private GameObject _endTilePrefab;
    [SerializeField]private GameObject _startTilePrefab;

    private Vector2Int _startTilePosition;
    private Vector2Int _endTilePosition;
    private Vector2Int[] _tilePositions;

	void Start()
    {
        LoadLevel(_currentLevelData);
	}

    public void LoadLevel(LevelData levelData)
    {
        _startTilePosition = levelData.startPosition;
		_endTilePosition = levelData.endPosition;
		_tilePositions = levelData.tilePositions;

        CreateTile();
	}

    private void CreateTile()
    {
		foreach (var pos in _tilePositions)
		{
			Instantiate(_tilePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity, transform);
		}
		Instantiate(_startTilePrefab, new Vector3(_startTilePosition.x, 0, _startTilePosition.y), Quaternion.identity, transform);
		Instantiate(_endTilePrefab, new Vector3(_endTilePosition.x, 0, _endTilePosition.y), Quaternion.identity, transform);
	}

    public void UnloadLevel()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

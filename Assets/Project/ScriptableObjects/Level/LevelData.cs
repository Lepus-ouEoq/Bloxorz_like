using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_00", menuName = "Bloxorz/Level Data")]
public class LevelData : ScriptableObject
{
	public string levelName;
	public Vector2Int startPosition;
	public Vector2Int endPosition;
	public Vector2Int[] tilePositions;

	[Header("Editor Info")]
	public bool isValidated;
	public bool isSolvable;
}

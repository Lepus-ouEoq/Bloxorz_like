using UnityEngine;

public class LevelValidator : MonoBehaviour
{
	private static LevelData _levelData;

	private void Start()
	{
		_levelData = GetComponent<LevelManager>().CurrentLevelData;
	}

	public static bool IsValidPosition(BlockData blockPosition)
	{
		if (_levelData == null || _levelData.tilePositions == null)
		{
			Debug.LogError("Level data is not initialized properly.");
			return false;
		}

		if (blockPosition.pose == BlockPose.Standing)
		{
			return IsTileInLevel( blockPosition.primaryTile[0]);
		}
		else if (blockPosition.pose == BlockPose.LyingX || blockPosition.pose == BlockPose.LyingZ)
		{
			bool tile0Valid = IsTileInLevel(blockPosition.primaryTile[0]);
			bool tile1Valid = IsTileInLevel(blockPosition.primaryTile[1]);
			
			return tile0Valid && tile1Valid;
		}

		return false;
	}

	private static bool IsTileInLevel(Vector2Int position)
	{
		if (position == _levelData.startPosition)
		{
			return true;
		}

		if (position == _levelData.endPosition)
		{
			return true;
		}

		foreach (var tilePos in _levelData.tilePositions)
		{
			if (tilePos == position)
			{
				return true;
			}
		}

		return false;
	}
}

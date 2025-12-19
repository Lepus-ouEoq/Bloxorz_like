using UnityEngine;

public enum BlockPose
{
	Standing,
	LyingX,
	LyingZ
}

public enum MoveDirection
{
	Forward,
	Back,
	Left,
	Right
}

public struct BlockData
{
	public Vector2Int[] primaryTile;
	public BlockPose pose;
}

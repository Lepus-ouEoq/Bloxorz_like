using System.Collections;
using UnityEngine;

public static class BlockPositionHelper
{
	// 座標陣列約定:
	// LyingX: primaryTile[0] = 左側格子, primaryTile[1] = 右側格子
	// LyingZ: primaryTile[0] = 下方格子, primaryTile[1] = 上方格子
	// Standing: primaryTile[0] = primaryTile[1] = 方塊所在格子
	
	/// <summary>
	/// 計算方塊翻滾後的新位置數據
	/// </summary>
	public static BlockData CalculateRoll(BlockData blockPosition, MoveDirection moveDirection)
	{
		Vector2Int vectorX;
		Vector2Int vectorY;
		
		if(blockPosition.pose == BlockPose.Standing)
		{
			if(moveDirection == MoveDirection.Forward)
			{
				// Standing 向前翻滾 -> LyingZ (下方, 上方)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.up;
				vectorY = blockPosition.primaryTile[0] + Vector2Int.up * 2;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingZ
				};
			}
			else if(moveDirection == MoveDirection.Back)
			{
				// Standing 向後翻滾 -> LyingZ (下方, 上方)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.down * 2;
				vectorY = blockPosition.primaryTile[0] + Vector2Int.down;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingZ
				};
			}
			else if(moveDirection == MoveDirection.Left)
			{
				// Standing 向左翻滾 -> LyingX (左側, 右側)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.left * 2;
				vectorY = blockPosition.primaryTile[0] + Vector2Int.left;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingX
				};
			}
			else if(moveDirection == MoveDirection.Right)
			{
				// Standing 向右翻滾 -> LyingX (左側, 右側)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.right;
				vectorY = blockPosition.primaryTile[0] + Vector2Int.right * 2;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingX
				};
			}
		}
		
		if(blockPosition.pose == BlockPose.LyingX)
		{
			if(moveDirection == MoveDirection.Forward)
			{
				// LyingX 向前移動，保持 LyingX
				vectorX = blockPosition.primaryTile[0] + Vector2Int.up;
				vectorY = blockPosition.primaryTile[1] + Vector2Int.up;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingX
				};
			}
			else if(moveDirection == MoveDirection.Back)
			{
				// LyingX 向後移動，保持 LyingX
				vectorX = blockPosition.primaryTile[0] + Vector2Int.down;
				vectorY = blockPosition.primaryTile[1] + Vector2Int.down;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingX
				};
			}
			else if(moveDirection == MoveDirection.Left)
			{
				// LyingX 向左翻滾 -> Standing (從左側格子翻)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.left;
				vectorY = vectorX;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.Standing
				};
			}
			else if(moveDirection == MoveDirection.Right)
			{
				// LyingX 向右翻滾 -> Standing (從右側格子翻)
				vectorX = blockPosition.primaryTile[1] + Vector2Int.right;
				vectorY = vectorX;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.Standing
				};
			}
		}
		
		if(blockPosition.pose == BlockPose.LyingZ)
		{
			if(moveDirection == MoveDirection.Forward)
			{
				// LyingZ 向前翻滾 -> Standing (從上方格子翻)
				vectorX = blockPosition.primaryTile[1] + Vector2Int.up;
				vectorY = vectorX;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.Standing
				};
			}
			else if(moveDirection == MoveDirection.Back)
			{
				// LyingZ 向後翻滾 -> Standing (從下方格子翻)
				vectorX = blockPosition.primaryTile[0] + Vector2Int.down;
				vectorY = vectorX;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.Standing
				};
			}
			else if(moveDirection == MoveDirection.Left)
			{
				// LyingZ 向左移動，保持 LyingZ
				vectorX = blockPosition.primaryTile[0] + Vector2Int.left;
				vectorY = blockPosition.primaryTile[1] + Vector2Int.left;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingZ
				};
			}
			else if(moveDirection == MoveDirection.Right)
			{
				// LyingZ 向右移動，保持 LyingZ
				vectorX = blockPosition.primaryTile[0] + Vector2Int.right;
				vectorY = blockPosition.primaryTile[1] + Vector2Int.right;
				return new BlockData
				{
					primaryTile = new Vector2Int[] { vectorX, vectorY },
					pose = BlockPose.LyingZ
				};
			}
		}
		
		return blockPosition;
	}
	
	/// <summary>
	/// 根據 BlockData 計算方塊在世界空間的中心位置
	/// </summary>
	public static Vector3 CalculateCenterPosition(BlockData blockData)
	{
		Vector2Int primaryTile = blockData.primaryTile[0];
		Vector2Int secondaryTile = blockData.primaryTile[1];
		
		switch (blockData.pose)
		{
			case BlockPose.LyingX:
				// X 軸躺平:兩個 tile 在 X 軸上並排
				return new Vector3(
					(primaryTile.x + secondaryTile.x) / 2f, 
					0.5f, 
					primaryTile.y
				);
				
			case BlockPose.LyingZ:
				// Z 軸躺平:兩個 tile 在 Z 軸上並排
				return new Vector3(
					primaryTile.x, 
					0.5f, 
					(primaryTile.y + secondaryTile.y) / 2f
				);
				
			case BlockPose.Standing:
			default:
				// 站立:只佔一個 tile
				return new Vector3(primaryTile.x, 1f, primaryTile.y);
		}
	}
}

using System;
using UnityEngine;
using DG.Tweening;

public class BlockController : MonoBehaviour
{
	[Header("Block Resouces")]
	[SerializeField] private GameObject _block;
	[SerializeField] private BlockAnimator _blockAnimator;

	[Header("Level Resources")]
	[SerializeField] private LevelManager _levelManager;

	private Vector3 _initialBlockPosition = new Vector3(0, 1f, 0);
	private BlockData _blockData;

	private void Start()
	{
		ResetBlock();
	}

	private void Update()
	{
		if (_blockAnimator != null && _blockAnimator.IsAnimating)
		{
			return;
		}

		MoveDirection? direction = GetInputDirection();
		
		if (direction.HasValue)
		{
			TryMove(direction.Value);
		}
	}

	public void ResetBlock()
	{
		_blockData = new BlockData
		{
			primaryTile = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 0) },
			pose = BlockPose.Standing
		};

		Vector3 startPosition = _initialBlockPosition;
		
		if (_blockAnimator != null)
		{
			_blockAnimator.SnapTo(startPosition, BlockPose.Standing);
		}
		else
		{
			_block.transform.position = startPosition;
		}
	}

	private MoveDirection? GetInputDirection()
	{
		if (Input.GetKeyDown(KeyCode.W)) return MoveDirection.Forward;
		if (Input.GetKeyDown(KeyCode.S)) return MoveDirection.Back;
		if (Input.GetKeyDown(KeyCode.A)) return MoveDirection.Left;
		if (Input.GetKeyDown(KeyCode.D)) return MoveDirection.Right;
		
		return null;
	}

	private void TryMove(MoveDirection direction)
	{
		BlockData previousBlock = _blockData;
		
		BlockData nextBlock = BlockPositionHelper.CalculateRoll(previousBlock, direction);
		
		_blockData = nextBlock;
			
		UpdateBlockVisualPosition(previousBlock, direction, nextBlock);

		if (!LevelValidator.IsValidPosition(nextBlock))
		{
			_blockData = previousBlock;
			WaitAndRollBack();
		}
	}

	private void UpdateBlockVisualPosition(BlockData currentBlock, MoveDirection direction, BlockData nextBlock)
	{
		Vector3 targetPosition = BlockPositionHelper.CalculateCenterPosition(nextBlock);
		
		if (_blockAnimator != null)
		{
			_blockAnimator.RollTo(
				targetPosition,
				currentBlock.pose,
				nextBlock.pose,
				direction
			);
		}
		else
		{
			_block.transform.position = targetPosition;
			Debug.LogWarning("BlockAnimator not assigned, snapping block to target position.");
		}
	}

	private void WaitAndRollBack()
	{
		if (_blockAnimator != null)
		{
			DOVirtual.DelayedCall(0f, () =>
			{
				if (_blockAnimator.IsAnimating)
				{
					WaitAndRollBack();
				}
				else
				{
					_blockAnimator.RollBack();
				}
			}).SetUpdate(true);
		}
		else
		{
			Debug.LogWarning("BlockAnimator not assigned, cannot update block visual position.");
		}
	}
}

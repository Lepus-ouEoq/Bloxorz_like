using DG.Tweening;
using UnityEngine;

public class BlockAnimator : MonoBehaviour
{
	[Header("Resources")]
	[SerializeField] private GameSettings _gameSettings;

	private bool _isAnimating = false;
	private Tween _currentTween;

	// Store the last roll information for rollback
	private Vector3 _lastStartPosition;
	private BlockPose _lastStartPose;
	private BlockPose _lastTargetPose;
	private MoveDirection _lastDirection;
	private bool _hasRollHistory = false;

	public bool IsAnimating => _isAnimating;

	public Tween RollTo(Vector3 targetPosition, BlockPose currentPose, BlockPose targetPose, MoveDirection direction)
	{
		if (_isAnimating) return null;

		_isAnimating = true;

		// Store current state for potential rollback
		_lastStartPosition = transform.position;
		_lastStartPose = currentPose;
		_lastTargetPose = targetPose;
		_lastDirection = direction;
		_hasRollHistory = true;

		Vector3 pivot = CalculatePivot(currentPose, direction);

		Vector3 axis = CalculateAxis(direction);

		float rotated = 0f;

		_currentTween = DOTween.To(
			() => rotated,
			angle =>
			{
				float delta = angle - rotated;
				transform.RotateAround(pivot, axis, delta);
				rotated = angle;
			},
			90f,
			_gameSettings.rollDuration
		)
		.SetEase(_gameSettings.rollEase)
		.OnComplete(() =>
		{
			SnapTo(targetPosition, targetPose);
			_isAnimating = false;
		});

		return _currentTween;
	}

	public Tween RollBack()
	{
		if (_isAnimating) return null;
		if (!_hasRollHistory) return null;

		_isAnimating = true;

		BlockPose currentPose = _lastTargetPose;

		Vector3 pivot = CalculatePivotForRollBack(currentPose, _lastDirection);

		Vector3 axis = CalculateAxis(_lastDirection);

		float rotated = 0f;

		_currentTween = DOTween.To(
			() => rotated,
			angle =>
			{
				float delta = angle - rotated;
				transform.RotateAround(pivot, axis, delta);
				rotated = angle;
			},
			-90f,
			_gameSettings.rollDuration
		)
		.SetEase(_gameSettings.rollEase)
		.OnComplete(() =>
		{
			SnapTo(_lastStartPosition, _lastStartPose);
			_isAnimating = false;
			_hasRollHistory = false;
		});

		return _currentTween;
	}

	private Vector3 CalculatePivot(BlockPose pose, MoveDirection direction)
	{
		Vector3 pos = transform.position;
		Vector3 dir = DirectionToVector(direction);

		float offsetAlongDir;
		float offsetDown;

		switch (pose)
		{
			case BlockPose.Standing:
				offsetAlongDir = 0.5f;
				offsetDown = 1f;
				break;

			case BlockPose.LyingX:
				if (direction == MoveDirection.Left || direction == MoveDirection.Right)
				{
					offsetAlongDir = 1f;
					offsetDown = 0.5f;
				}
				else
				{
					offsetAlongDir = 0.5f;
					offsetDown = 0.5f;
				}
				break;

			case BlockPose.LyingZ:
				if (direction == MoveDirection.Forward || direction == MoveDirection.Back)
				{
					offsetAlongDir = 1f;
					offsetDown = 0.5f;
				}
				else
				{
					offsetAlongDir = 0.5f;
					offsetDown = 0.5f;
				}
				break;

			default:
				offsetAlongDir = 0.5f;
				offsetDown = 0.5f;
				break;
		}

		return pos + dir * offsetAlongDir + Vector3.down * offsetDown;
	}

	private Vector3 CalculatePivotForRollBack(BlockPose pose, MoveDirection direction)
	{
		Vector3 pos = transform.position;
		Vector3 dir = -DirectionToVector(direction);

		float offsetAlongDir;
		float offsetDown;

		switch (pose)
		{
			case BlockPose.Standing:
				offsetAlongDir = 0.5f;
				offsetDown = 1f;
				break;

			case BlockPose.LyingX:
				if (direction == MoveDirection.Left || direction == MoveDirection.Right)
				{
					offsetAlongDir = 1f;
					offsetDown = 0.5f;
				}
				else
				{
					offsetAlongDir = 0.5f;
					offsetDown = 0.5f;
				}
				break;

			case BlockPose.LyingZ:
				if (direction == MoveDirection.Forward || direction == MoveDirection.Back)
				{
					offsetAlongDir = 1f;
					offsetDown = 0.5f;
				}
				else
				{
					offsetAlongDir = 0.5f;
					offsetDown = 0.5f;
				}
				break;

			default:
				offsetAlongDir = 0.5f;
				offsetDown = 0.5f;
				break;
		}

		return pos + dir * offsetAlongDir + Vector3.down * offsetDown;
	}

	private Vector3 CalculateAxis(MoveDirection direction)
	{
		return direction switch
		{
			MoveDirection.Forward => Vector3.right,
			MoveDirection.Back => Vector3.left,
			MoveDirection.Right => Vector3.back,
			MoveDirection.Left => Vector3.forward,
			_ => Vector3.right
		};
	}

	private Vector3 DirectionToVector(MoveDirection direction)
	{
		return direction switch
		{
			MoveDirection.Forward => Vector3.forward,  // (0, 0, 1)
			MoveDirection.Back => Vector3.back,        // (0, 0, -1)
			MoveDirection.Left => Vector3.left,        // (-1, 0, 0)
			MoveDirection.Right => Vector3.right,      // (1, 0, 0)
			_ => Vector3.zero
		};
	}

	public void SnapTo(Vector3 targetPosition, BlockPose targetPose)
	{
		Vector3 pos = targetPosition;
		pos.x = Mathf.Round(pos.x * 2f) / 2f;
		pos.y = Mathf.Round(pos.y * 2f) / 2f;
		pos.z = Mathf.Round(pos.z * 2f) / 2f;
		transform.position = pos;

		Vector3 euler = transform.eulerAngles;
		euler.x = Mathf.Round(euler.x / 90f) * 90f;
		euler.y = Mathf.Round(euler.y / 90f) * 90f;
		euler.z = Mathf.Round(euler.z / 90f) * 90f;
		transform.eulerAngles = euler;
	}

	public void CancelAnimation()
	{
		_currentTween?.Kill();
		_isAnimating = false;
	}

	private void OnDestroy()
	{
		_currentTween?.Kill();
	}
}
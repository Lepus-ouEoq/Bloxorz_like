using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Bloxorz/Game Settings")]
public class GameSettings : ScriptableObject
{
	[Header("Block")]
	public float rollDuration = 0.3f;
	// 方塊翻滾動畫的緩動曲線類型，OutQuad 表示二次方緩出效果（開始快，結束慢）
	public Ease rollEase = Ease.OutQuad;
	
	[Header("Input")]
	public float swipeThreshold = 50f;
	public float swipeMaxTime = 0.5f;

	[Header("Camera")]
	public float rotationDuration = 0.3f;
	// 初始俯仰角度
	public float initialPitch = 45f;
	// 初始偏航角度
	public float initialYaw = 45f;
}

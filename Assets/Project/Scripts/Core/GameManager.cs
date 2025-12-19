using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	#region Singleton
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
				if (_instance == null)
				{
					GameObject gm = new GameObject("GameManager");
					_instance = gm.AddComponent<GameManager>();
					DontDestroyOnLoad(gm);
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		// 確保場景中只存在一個 GameManager 實例
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (_instance != this)
		{
			// 如果已經存在其他實例，銷毀當前物件
			Destroy(gameObject);
		}
	}
	#endregion

	[Header("References")]
	[SerializeField] private BlockController _blockController;
	[SerializeField] private LevelManager _levelManager;

	private void OnLevelComplete()
	{
		Debug.Log("關卡完成！");
	}

	private void OnLevelReset()
	{
		_levelManager.UnloadLevel();
		_levelManager.LoadLevel(_levelManager.CurrentLevelData);
		_blockController.ResetBlock();
	}
}

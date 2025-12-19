using System.Collections.Generic;
using UnityEngine;

namespace RollSample.Spec
{
	public class OutputLog
	{
		//紀錄每次移動後的方塊位置(所有覆蓋的地格)
		public List<List<Vector2>> StepPositions;
		//通關與否
		public bool IsClear;
	}
}

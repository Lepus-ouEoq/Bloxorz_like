using System.Collections.Generic;
using UnityEngine;

namespace RollSample.Spec
{
	public class InputData
	{
		//左上角為(0,0) 往下x增加 往右y增加
		public List<Vector2> MapData;
		//起始與結束皆為直立
		public Vector2 StartPosition;
		public Vector2 EndPosition;

		//移動步驟
		public List<MoveDirection> MoveSteps;
	}
}

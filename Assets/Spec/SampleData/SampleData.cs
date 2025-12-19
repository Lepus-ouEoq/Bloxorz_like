using System.Collections.Generic;
using UnityEngine;

namespace RollSample.Spec.SampleData
{
	public class SampleData
	{
		public static InputData GetInputSample()
		{
			InputData inputData = new InputData();
			inputData.MapData = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2), new Vector2(2, 0), new Vector2(2, 1), new Vector2(2, 2) };
			inputData.StartPosition = new Vector2(0, 0);
			inputData.EndPosition = new Vector2(2, 2);

			inputData.MoveSteps = new List<MoveDirection>() { MoveDirection.Down, MoveDirection.Right, MoveDirection.Right, MoveDirection.Up, MoveDirection.Left, MoveDirection.Down, MoveDirection.Down, MoveDirection.Right, };
			return inputData;
		}

		public static OutputLog GetOutputSample()
		{
			OutputLog ret = new OutputLog();
			ret.StepPositions = new List<List<Vector2>> { new List<Vector2> { new Vector2(0, 0) }, new List<Vector2> { new Vector2(1, 0), new Vector2(2, 0) }, new List<Vector2> { new Vector2(1, 1), new Vector2(2, 1) }, new List<Vector2> { new Vector2(1, 2), new Vector2(2, 2) }, new List<Vector2> { new Vector2(0, 2) }, new List<Vector2> { new Vector2(0, 0), new Vector2(0, 1) }, new List<Vector2> { new Vector2(1, 0), new Vector2(1, 1) }, new List<Vector2> { new Vector2(2, 0), new Vector2(2, 1) }, new List<Vector2> { new Vector2(2, 2) } };
			ret.IsClear = true;
			return ret;
		}
	}
}

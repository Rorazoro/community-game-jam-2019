/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class TaskIntCompletionData : TaskCompletionData
{
	public int Value;

	public TaskIntCompletionData(int value)
	{
		this.Value = value;
	}
}

[CreateAssetMenu(fileName = "Task Int", menuName = "Task/Task Int", order = 1)]
public class TaskInt : Task<TaskIntData, TaskIntCompletionData>
{
	private int _value;

	[SerializeField] private int _completionValue = 1;

	protected override bool TryToComplete(TaskIntCompletionData taskCompletionData)
	{
		this._value += taskCompletionData.Value;

		if (this._value >= this._completionValue)
			return true;

		return false;
	}

	public override void ResetState()
	{
		base.ResetState();

		this._value = this.taskData._Value;
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TaskInt))]
[CanEditMultipleObjects]
public class TaskIntEditor : Editor
{
#pragma warning disable 0219, 414
	private TaskInt _sTaskInt;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTaskInt = this.target as TaskInt;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
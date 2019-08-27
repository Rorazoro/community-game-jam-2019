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

[CreateAssetMenu(fileName = "Task Int Data", menuName = "Task/Data/Task Int Data", order = 1)]
public class TaskIntData : DefaultTaskData
{
	[SerializeField] private int _countValue;
	public int _Value { get { return this._countValue; } }

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TaskIntData))]
[CanEditMultipleObjects]
public class TaskIntDataEditor : Editor
{
#pragma warning disable 0219, 414
	private TaskIntData _sTaskIntData;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTaskIntData = this.target as TaskIntData;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

[CreateAssetMenu(fileName = "name Complex Task", menuName = "Task/Complex Task", order = 1)]
public class ComplexTask : Task
{
	[SerializeField] private TaskData _taskData;
	public TaskData _TaskData => this._taskData;

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ComplexTask))]
[CanEditMultipleObjects]
public class ComplexTaskEditor : Editor
{
#pragma warning disable 0219, 414
	private ComplexTask _sComplexTask;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sComplexTask = this.target as ComplexTask;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
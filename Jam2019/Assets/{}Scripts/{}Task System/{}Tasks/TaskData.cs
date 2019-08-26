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

[System.Serializable]
[CreateAssetMenu(fileName = "Task Data", menuName = "Task/Data/Task Data", order = 1)]
public class TaskData : ScriptableObject
{
#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TaskData))]
[CanEditMultipleObjects]
public class TaskDataEditor : Editor
{
#pragma warning disable 0219, 414
	private TaskData _sTaskData;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTaskData = this.target as TaskData;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
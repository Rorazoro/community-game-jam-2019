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

//[CreateAssetMenu(fileName = "Default Task Data", menuName = "Task/Data/Default Task Data", order = 1)]
public class DefaultTaskData : ScriptableObject
{
#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(DefaultTaskData))]
[CanEditMultipleObjects]
public class DefaultTaskDataEditor : Editor
{
#pragma warning disable 0219, 414
	private DefaultTaskData _sDefaultTaskData;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sDefaultTaskData = this.target as DefaultTaskData;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
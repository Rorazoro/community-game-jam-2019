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

using UDebug = UnityEngine.Debug;

public class Debugger : MonoBehaviour
{
	public void Debug(string message) => UDebug.Log(message);
	public void Debug(object message) => UDebug.Log(message);

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Debugger))]
[CanEditMultipleObjects]
public class DebuggerEditor : Editor
{
#pragma warning disable 0219, 414
	private Debugger _sDebugger;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sDebugger = this.target as Debugger;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
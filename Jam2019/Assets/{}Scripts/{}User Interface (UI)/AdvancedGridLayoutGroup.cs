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

public class AdvancedGridLayoutGroup : GridLayoutGroup
{
#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(AdvancedGridLayoutGroup))]
[CanEditMultipleObjects]
public class AdvancedGridLayoutGroupEditor : Editor
{
#pragma warning disable 0219, 414
	private AdvancedGridLayoutGroup _sAdvancedGridLayoutGroup;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sAdvancedGridLayoutGroup = this.target as AdvancedGridLayoutGroup;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
﻿/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class ColorSetter : MonoBehaviour
{
	[SerializeField] private Graphic _graphics;

	[SerializeField] private Color[] _colors;

	public void SetColor(int colorIndex)
	{
		this._graphics.color = this._colors[colorIndex];
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._graphics = this.GetComponent<Graphic>();
	}

	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorSetter))]
[CanEditMultipleObjects]
public class ColorSetterEditor : Editor
{
#pragma warning disable 0219, 414
	private ColorSetter _sColorSetter;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sColorSetter = this.target as ColorSetter;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
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

public class GradientSetter : MonoBehaviour
{
	[SerializeField] private Graphic _graphics;

	[SerializeField] private Gradient _gradient;

	public void Evaluate(float time)
	{
		this._graphics.color = this._gradient.Evaluate(time);
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
[CustomEditor(typeof(GradientSetter))]
[CanEditMultipleObjects]
public class GradientSetterEditor : Editor
{
#pragma warning disable 0219, 414
	private GradientSetter _sGradientSetter;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sGradientSetter = this.target as GradientSetter;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
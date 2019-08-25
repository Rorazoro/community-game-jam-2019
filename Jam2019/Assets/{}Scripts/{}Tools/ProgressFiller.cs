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

public abstract class ProgressFiller : MonoBehaviour
{
	[SerializeField] private UnityEventFloat _onFillChanged;
	public UnityEventFloat _OnFillChanged { get { return this._onFillChanged; } }

	protected abstract void OnFillChanged(float fill);

	[Range(0f, 1f)]
	[SerializeField] protected float fill = 0.75f;
	public float Fill { get { return this.fill; } set { this.fill = Mathf.Clamp01(value); this._onFillChanged.Invoke(this.fill); } }

	protected virtual void Awake()
	{
		this._onFillChanged.AddListener(this.OnFillChanged);
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ProgressFiller))]
[CanEditMultipleObjects]
public class ProgressFillerEditor : Editor
{
#pragma warning disable 0219, 414
	private ProgressFiller _sProgressFiller;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sProgressFiller = this.target as ProgressFiller;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
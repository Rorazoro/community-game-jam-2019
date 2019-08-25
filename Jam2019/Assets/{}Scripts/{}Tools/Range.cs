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
public abstract class Range<TValue>
{
	public TValue Min;
	public TValue Max;

	public abstract TValue GetRandom();

#if UNITY_EDITOR
#endif
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(Range))]
//[CanEditMultipleObjects]
//public class RangeEditor : Editor
//{
//#pragma warning disable 0219, 414
//	private Range _sRange;
//#pragma warning restore 0219, 414

//	private void OnEnable()
//	{
//		this._sRange = this.target as Range;
//	}

//	public override void OnInspectorGUI()
//	{
//		this.DrawDefaultInspector();
//	}
//}
//#endif
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
public class RangeFloat : Range<float>
{
	public override float GetRandom()
	{
		return Random.Range(this.Min, this.Max);
	}

#if UNITY_EDITOR
#endif
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(RangeFloat))]
//[CanEditMultipleObjects]
//public class RangeFloatEditor : Editor
//{
//#pragma warning disable 0219, 414
//	private RangeFloat _sRangeFloat;
//#pragma warning restore 0219, 414

//	private void OnEnable()
//	{
//		this._sRangeFloat = this.target as RangeFloat;
//	}

//	public override void OnInspectorGUI()
//	{
//		this.DrawDefaultInspector();
//	}
//}
//#endif
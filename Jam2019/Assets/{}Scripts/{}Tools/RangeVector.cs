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
public abstract class RangeVector<TVector> : Range<TVector>
{
	public abstract TVector GetRandomPointOnLine();

#if UNITY_EDITOR
#endif
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(RangeVector))]
//[CanEditMultipleObjects]
//public class RangeVectorEditor : Editor
//{
//#pragma warning disable 0219, 414
//	private RangeVector _sRangeVector;
//#pragma warning restore 0219, 414

//	private void OnEnable()
//	{
//		this._sRangeVector = this.target as RangeVector;
//	}

//	public override void OnInspectorGUI()
//	{
//		this.DrawDefaultInspector();
//	}
//}
//#endif
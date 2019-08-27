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
public class RangeVector2 : RangeVector<Vector2>
{
	public override Vector2 GetRandomPointOnLine()
	{
		return Vector2.Lerp(this.Min, this.Max, Random.value);
	}

	public override Vector2 GetRandom()
	{
		return new Vector2(Random.Range(this.Min.x, this.Max.x), Random.Range(this.Min.y, this.Max.y));
	}

#if UNITY_EDITOR
#endif
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(RangeVector2))]
//[CanEditMultipleObjects]
//public class RangeVector2Editor : Editor
//{
//#pragma warning disable 0219, 414
//	private RangeVector2 _sRangeVector2;
//#pragma warning restore 0219, 414

//	private void OnEnable()
//	{
//		this._sRangeVector2 = this.target as RangeVector2;
//	}

//	public override void OnInspectorGUI()
//	{
//		this.DrawDefaultInspector();
//	}
//}
//#endif
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
public class RangeVector3 : RangeVector<Vector3>
{
	public override Vector3 GetRandomPointOnLine()
	{
		return Vector3.Lerp(this.Min, this.Max, Random.value);
	}

	public override Vector3 GetRandom()
	{
		return new Vector3(Random.Range(this.Min.x, this.Max.x), Random.Range(this.Min.y, this.Max.y), Random.Range(this.Min.z, this.Max.z));
	}

#if UNITY_EDITOR
#endif
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(RangeVector3))]
//[CanEditMultipleObjects]
//public class RangeVector3Editor : Editor
//{
//#pragma warning disable 0219, 414
//	private RangeVector3 _sRangeVector3;
//#pragma warning restore 0219, 414

//	private void OnEnable()
//	{
//		this._sRangeVector3 = this.target as RangeVector3;
//	}

//	public override void OnInspectorGUI()
//	{
//		this.DrawDefaultInspector();
//	}
//}
//#endif
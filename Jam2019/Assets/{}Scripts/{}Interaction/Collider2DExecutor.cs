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

public class Collider2DExecutor : MonoBehaviour
{
	[SerializeField] private Collider2D _collider;
	[SerializeField] private LayeredUnityEventCollider2DTriggerData[] _onOverlapColliderData;

	public void OverlapCollider()
	{
		for (int a = 0; a < this._onOverlapColliderData.Length; a++)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				useLayerMask = true
			};
			contactFilter2D.SetLayerMask(this._onOverlapColliderData[a]._LayerMask);

			Collider2D[] collider2Ds = new Collider2D[8];

			int collidersQuantity = this._collider.OverlapCollider(contactFilter2D, collider2Ds);

			for (int b = 0; b < collidersQuantity; b++)
				this._onOverlapColliderData[a]._Event.Invoke(collider2Ds[b]);
		}
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._collider = this.GetComponent<Collider2D>();
	}

	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Collider2DExecutor))]
[CanEditMultipleObjects]
public class Collider2DExecutorEditor : Editor
{
#pragma warning disable 0219, 414
	private Collider2DExecutor _sCollider2DExecutor;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sCollider2DExecutor = this.target as Collider2DExecutor;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
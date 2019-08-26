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

public class WorldSpaceDialogueView : DialogueView
{
	private Transform _target;

	public void SetTarget(Transform target)
	{
		this._target = target;
	}

	public void DisplayDialogue(Dialogue dialogue, Transform target)
	{
		if (this._target == null)
		{
			Debug.LogError("Target is not set.");
			return;
		}

		this.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(this._target.position);

		base.DisplayDialogue(dialogue);
	}

	public override void DisplayDialogue(Dialogue dialogue) => this.DisplayDialogue(dialogue, this._target);

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(WorldSpaceDialogueView))]
[CanEditMultipleObjects]
public class WorldSpaceDialogueViewEditor : Editor
{
#pragma warning disable 0219, 414
	private WorldSpaceDialogueView _sWorldSpaceDialogueView;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sWorldSpaceDialogueView = this.target as WorldSpaceDialogueView;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
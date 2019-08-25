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

public interface IInteractable
{
	void Interact();
	void Interact(Object @object);

#if UNITY_EDITOR
#endif
}

public interface IInteractable<TComponent> : IInteractable
	where TComponent : Component
{
	void Interact(TComponent component);

#if UNITY_EDITOR
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(IInteractable))]
[CanEditMultipleObjects]
public class IInteractableEditor : Editor
{
#pragma warning disable 0219, 414
	private IInteractable _sIInteractable;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sIInteractable = this.target as IInteractable;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class InputActionReferenceExecutor : InputActionExecutor<InputActionReference>
{
	protected override void InitializeActions()
	{
		this.inputAction.action.started += this.onActionStarted.Invoke;
		this.inputAction.action.performed += this.onActionPerformed.Invoke;
		this.inputAction.action.canceled += this.onActionCanceled.Invoke;

		this.inputAction.action.Enable();
	}

	protected override void DeItializeActions()
	{
		this.inputAction.action.started -= this.onActionStarted.Invoke;
		this.inputAction.action.performed -= this.onActionPerformed.Invoke;
		this.inputAction.action.canceled -= this.onActionCanceled.Invoke;

		this.inputAction.action.Disable();
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(InputActionReferenceExecutor))]
[CanEditMultipleObjects]
public class InputActionReferenceExecutorEditor : Editor
{
#pragma warning disable 0219, 414
	private InputActionReferenceExecutor _sInputActionReferenceExecutor;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sInputActionReferenceExecutor = this.target as InputActionReferenceExecutor;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

[RequireComponent(typeof(EventSystem))]
public class EventSystemController : MonoBehaviour
{
	private EventSystem _eventSystem;

	[SerializeField] private GameObject _defaultSelection;

	public void Deselect() => this._eventSystem.SetSelectedGameObject(null);

	public void Select(GameObject gameObject) => this._eventSystem.SetSelectedGameObject(gameObject);
	public void Select() => this.Select(this._defaultSelection);

	private void EnsureSelection(InputAction.CallbackContext callbackContext)
	{
		if (this._eventSystem.currentSelectedGameObject is null)
			this.Select();
	}

	[SerializeField] private InputActionAsset _inputActions;
	private InputActionMap _userInterfaceInputActions;

	private void Awake()
	{
		this._eventSystem = this.GetComponent<EventSystem>();

		this._inputActions = this.GetComponent<InputSystemUIInputModule>().actionsAsset;
		this._userInterfaceInputActions = this._inputActions.GetActionMap("UI");

		InputAction navigate = this._userInterfaceInputActions.GetAction("Navigate");

		navigate.canceled += this.EnsureSelection;

		this.Deselect();
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._inputActions = this.GetComponent<InputSystemUIInputModule>().actionsAsset;
	}

	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(EventSystemController))]
[CanEditMultipleObjects]
public class EventSystemControllerEditor : Editor
{
#pragma warning disable 0219, 414
	private EventSystemController _sEventSystemController;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sEventSystemController = this.target as EventSystemController;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
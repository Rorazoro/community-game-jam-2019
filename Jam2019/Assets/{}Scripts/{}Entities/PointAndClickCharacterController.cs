/* Created by Luna.Ticode */

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

using TMPro;

using Object = UnityEngine.Object;

public class ActionGroup
{
	private const int _DEFAULT_ACTION_LIST_SIZE = 4;

	private List<Action> _actions = new List<Action>(ActionGroup._DEFAULT_ACTION_LIST_SIZE);

	public void Add(Action action) => this._actions.Add(action);

	public bool Remove(Action action)
	{
		for (int a = 0; a < this._actions.Count; a++)
		{
			if (this._actions[a] == action)
			{
				this._actions.RemoveAt(a);

				return true;
			}
		}

		return false;
	}

	public void Invoke()
	{
		for (int a = 0; a < this._actions.Count; a++)
			this._actions[a].Invoke();
	}

	public void Clear()
	{
		if (this._actions.Count > 0)
			this._actions.Clear();
	}
}

[RequireComponent(typeof(NavMeshAgent))]
public class PointAndClickCharacterController : MonoBehaviour
{
	private NavMeshAgent _navMeshAgent;

	[SerializeField] private Transform _destinationIndicatorPrefab;
	[SerializeField] private LayerMask _walkableLayerMask;

	private Transform _destinationIndicator;

	[SerializeField] private UnityEvent _onDestinationReachedPersistent;
	private ActionGroup _onDestinationReachedDisposable = new ActionGroup();

	private IEnumerator ValidatePosition()
	{
		while ((this.transform.position - this._destinationIndicator.position).sqrMagnitude > 1f)
		{
			yield return null;
		}

		this._onDestinationReachedPersistent.Invoke();
		
		this._onDestinationReachedDisposable.Invoke();
		this._onDestinationReachedDisposable.Clear();
	}

	[SerializeField] private UnityEvent _onDestinationChange;

	private Coroutine _destinationReachCoroutine;

	private void SetDestination()
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 1000f, this._walkableLayerMask))
		{
			this._onDestinationChange.Invoke();

			Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

			Vector3 destination;

			if (interactable != null)
			{
				destination = interactable._InteractionPoint.position;
				
				this._onDestinationReachedDisposable.Add(interactable.Interact);
			}
			else
				destination = hitInfo.point;

			this._navMeshAgent.SetDestination(destination);

			this._destinationIndicator = Object.Instantiate(this._destinationIndicatorPrefab, destination, Quaternion.identity);

			if (this._destinationReachCoroutine != null)
				this.StopCoroutine(this._destinationReachCoroutine);

			this._destinationReachCoroutine = this.StartCoroutine(this.ValidatePosition());
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()) //TODO: over UI?
		{
			this.SetDestination();
		}
	}

	private void Awake()
	{
		this._navMeshAgent = this.GetComponent<NavMeshAgent>();

		this._onDestinationChange.AddListener(
			() =>
			{
				if (this._destinationIndicator != null)
					Object.Destroy(this._destinationIndicator.gameObject);
			}
		);

		this._onDestinationChange.AddListener(this._onDestinationReachedDisposable.Clear);
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(PointAndClickCharacterController))]
[CanEditMultipleObjects]
public class PointAndClickCharacterControllerEditor : Editor
{
#pragma warning disable 0219, 414
	private PointAndClickCharacterController _sPointAndClickCharacterController;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sPointAndClickCharacterController = this.target as PointAndClickCharacterController;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
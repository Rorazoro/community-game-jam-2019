/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class InteractionTrigger : MonoBehaviour
{
	[SerializeField] private LayeredUnityEventColliderTriggerData[] _onTriggerEnterData;
	[SerializeField] private LayeredUnityEventColliderTriggerData[] _onTriggerStayData;
	[SerializeField] private LayeredUnityEventColliderTriggerData[] _onTriggerExitData;

	private Coroutine _onTriggerStayProcess;

	private IEnumerator OnTriggerStayProcess(Collider other)
	{
		while (true)
		{
			for (int i = 0; i < this._onTriggerStayData.Length; i++)
			{
				if (this._onTriggerStayData[i]._LayerMask.Contains(other.gameObject))
					this._onTriggerStayData[i]._Event.Invoke(other);
			}

			yield return null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		for (int i = 0; i < this._onTriggerEnterData.Length; i++)
		{
			if (this._onTriggerEnterData[i]._LayerMask.Contains(other.gameObject))
				this._onTriggerEnterData[i]._Event.Invoke(other);
		}

		this._onTriggerStayProcess = this.StartCoroutine(this.OnTriggerStayProcess(other));
	}

	// Isn't called every frame, thus things like input aren't working properly.
	//private void OnTriggerStay(Collider other)
	//{

	//}

	private void OnTriggerExit(Collider other)
	{
		for (int i = 0; i < this._onTriggerExitData.Length; i++)
		{
			if (this._onTriggerExitData[i]._LayerMask.Contains(other.gameObject))
				this._onTriggerExitData[i]._Event.Invoke(other);
		}

		this.StopCoroutine(this._onTriggerStayProcess);
	}

#if UNITY_EDITOR
	private const float GIZMO_SIZE_BIAS = 0.1f;

	private BoxCollider _defaultCollider;

	[ContextMenu("Reset Collision Data")]
	public void CheckForTriggerColliders()
	{
		foreach (Collider collider in this.GetComponents<Collider>())
		{
			if (collider.isTrigger)
			{
				this._defaultCollider = collider as BoxCollider;
				return;
			}
		}

		this._defaultCollider = this.gameObject.AddComponent<BoxCollider>();
		this._defaultCollider.isTrigger = true;
	}

	[Header("Unity Editor Only")]
	[SerializeField] private Vector3 _gizmoSize = Vector3.one;

	private bool _triedToFindDefaultCollider;

	protected virtual void OnDrawGizmos()
	{
		if (!this._triedToFindDefaultCollider)
		{
			this.CheckForTriggerColliders();

			this._triedToFindDefaultCollider = true;
		}

		Color color = Color.cyan;
		color.a = 0.2f;
		Gizmos.color = color;

		if (this._defaultCollider != null)
		{
			Gizmos.DrawCube(this.transform.position + Vector3.Scale(this._defaultCollider.center, this.transform.localScale), Vector3.Scale(this._defaultCollider.size, this.transform.localScale) + Vector3.one * GIZMO_SIZE_BIAS);
		}
		else
		{
			Gizmos.DrawCube(this.transform.position, this._gizmoSize + Vector3.one * GIZMO_SIZE_BIAS);
		}
	}

	private void Reset()
	{
		InputActionExecutor inputActionExecutor = this.GetComponent<InputActionExecutor>();
		if (inputActionExecutor != null)
		{
			this._onTriggerEnterData = new LayeredUnityEventColliderTriggerData[]
			{ new LayeredUnityEventColliderTriggerData(new UnityEventCollider(), new LayerMask()) };

			UnityEventToolsExpansion.AddPersistentListener(this._onTriggerEnterData[0]._Event, inputActionExecutor.Activate);

			this._onTriggerExitData = new LayeredUnityEventColliderTriggerData[]
			{ new LayeredUnityEventColliderTriggerData(new UnityEventCollider(), new LayerMask()) };

			UnityEventToolsExpansion.AddPersistentListener(this._onTriggerExitData[0]._Event, inputActionExecutor.Deactivate);
		}
	}
#endif
}

[System.Serializable]
public class LayeredEventTriggerData<TEvent>
	where TEvent : UnityEventBase
{
	[SerializeField] private TEvent _event;
	public TEvent _Event { get { return this._event; } }

	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask { get { return this._layerMask; } }

	public LayeredEventTriggerData(TEvent @event, LayerMask layerMask)
	{
		this._event = @event;
		this._layerMask = layerMask;
	}
}

[System.Serializable]
public class LayeredUnityEventColliderTriggerData : LayeredEventTriggerData<UnityEventCollider>
{
	public LayeredUnityEventColliderTriggerData(UnityEventCollider @event, LayerMask layerMask) : base(@event, layerMask)
	{
	}
}

[System.Serializable]
public class LayeredUnityEventCollider2DTriggerData : LayeredEventTriggerData<UnityEventCollider2D>
{
	public LayeredUnityEventCollider2DTriggerData(UnityEventCollider2D @event, LayerMask layerMask) : base(@event, layerMask)
	{
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(InteractionTrigger))]
[CanEditMultipleObjects]
public class InteractionTriggerEditor : Editor
{
#pragma warning disable 0219
	private InteractionTrigger _sInteractionTrigger;
#pragma warning restore 0219

	private void OnEnable()
	{
		this._sInteractionTrigger = this.target as InteractionTrigger;

		this._sInteractionTrigger.CheckForTriggerColliders();
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();

		foreach (Collider collider in this._sInteractionTrigger.GetComponents<Collider>())
		{
			if (collider.isTrigger)
			{
				return;
			}
		}

		EditorGUILayout.HelpBox("There is no trigger collider attached. Events might not trigger", MessageType.Warning); //TODO: Search for colliders recursively
	}
}
#endif
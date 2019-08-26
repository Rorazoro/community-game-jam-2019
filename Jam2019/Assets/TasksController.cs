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

public class TasksController : MonoBehaviourSingleton<TasksController>
{
	[SerializeField] private Task[] _tasks;

	protected override void Awake()
	{
		base.Awake();

		for (int a = 0; a < this._tasks.Length; a++)
		{
			this._tasks[a].ResetState();
			this._tasks[a].Initialize();
		}
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TasksController))]
[CanEditMultipleObjects]
public class TasksControllerEditor : Editor
{
#pragma warning disable 0219, 414
	private TasksController _sTasksController;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTasksController = this.target as TasksController;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
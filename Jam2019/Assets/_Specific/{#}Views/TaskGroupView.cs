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

public class TaskGroupView : View
{
	[SerializeField] private ComplexTask[] _testTasks;

	[SerializeField] private TaskView _taskViewPrefab;

	public void Display(ComplexTask[] tasks)
	{
		for (int a = 0; a < tasks.Length; a++)
		{
			TaskView taskView = Object.Instantiate(this._taskViewPrefab, this.content);
			taskView.Display(tasks[a]);
		}
	}

	protected override void Start()
	{
		base.Start();

		this.Display(this._testTasks);
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TaskGroupView))]
[CanEditMultipleObjects]
public class TaskGroupViewEditor : ViewEditor
{
#pragma warning disable 0219, 414
	private TaskGroupView _sTaskGroupView;
#pragma warning restore 0219, 414

	protected override void OnEnable()
	{
		base.OnEnable();

		this._sTaskGroupView = this.target as TaskGroupView;
	}
}
#endif
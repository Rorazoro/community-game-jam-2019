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

public class TaskView : View
{
	private ComplexTask _task;

	[SerializeField] private ImageToggle _imageToggle;
	[SerializeField] private TextMeshProUGUI _description;

	public void UpdateLayout()
	{
		this._imageToggle.Toggle(this._task._Completed);

		this._description.text = this._task._TaskData._Description;
	}

	public void Display(ComplexTask task)
	{
		if (this._task != null)
			this._task.OnComplete -= this.UpdateLayout;

		this._task = task;
		this._task.OnComplete += this.UpdateLayout;

		this.UpdateLayout();
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TaskView))]
[CanEditMultipleObjects]
public class TaskViewEditor : ViewEditor
{
#pragma warning disable 0219, 414
	private TaskView _sTaskView;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTaskView = this.target as TaskView;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
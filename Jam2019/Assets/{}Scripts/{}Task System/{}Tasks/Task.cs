/* Created by Luna.Ticode */

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Task<TDefaultTaskData, TTaskCompletionData> : Task
	where TDefaultTaskData : DefaultTaskData
	where TTaskCompletionData : TaskCompletionData
{
	[SerializeField] protected TDefaultTaskData taskData;

	protected abstract bool TryToComplete(TTaskCompletionData taskCompletionData);

	public sealed override bool TryToComplete<TTaskCompletionDataInternal>(TTaskCompletionDataInternal taskCompletionData)
	{
		if (this.TryToComplete(taskCompletionData as TTaskCompletionData))
		{
			this.Complete();

			return true;
		}

		return false;
	}
}

public interface ITask
{
	bool _Active { get; }

	bool _Completed { get; }
	void Complete(bool includeSubTasks = false);

	bool TryToCompleteDependingOnSubTasks();
}

[CreateAssetMenu(fileName = "s Task", menuName = "Task/Task", order = 1)]
public class Task : ScriptableObject, ITask
{
	[SerializeField] protected Task[] subTasks;

	public Task ParentTask { get; set; }

	[SerializeField] protected bool active = true;
	public bool _Active { get { return this.active; } }

	public void Activate()
	{
		this.active = true;
	}

	[SerializeField] protected bool completed;
	public bool _Completed { get { return this.completed; } }

	public event Action OnComplete = delegate { };
	
	public void Complete(bool includeSubTasks = false)
	{
		if (includeSubTasks)
		{
			for (int i = 0; i < this.subTasks.Length; i++)
			{
				this.subTasks[i].Complete(true);
			}
		}

		this.completed = true;

		this.OnComplete.Invoke();

		this.ParentTask?.TryToCompleteDependingOnSubTasks();
	}

	public virtual bool TryToCompleteDependingOnSubTasks()
	{
		if (this.completed)
			return true;
		else if (!this.active)
			return false;

		if (this.subTasks.Length > 0)
		{
			for (int i = 0; i < this.subTasks.Length; i++)
			{
				if (!this.subTasks[i].completed)
					return false;
			}
		}

		this.Complete();

		return true;
	}

	public virtual bool TryToComplete<TTaskCompletionData>(TTaskCompletionData taskCompletionData)
		where TTaskCompletionData : TaskCompletionData
	{
		return this.TryToCompleteDependingOnSubTasks();
	}

	public virtual void ResetState()
	{
		this.completed = false;

		for (int i = 0; i < this.subTasks.Length; i++)
		{
			this.subTasks[i].ResetState();
		}
	}
	
	public virtual void Initialize()
	{
		for (int i = 0; i < this.subTasks.Length; i++)
		{
			this.subTasks[i].Initialize();
			
			this.subTasks[i].ParentTask = this;
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Task))]
[CanEditMultipleObjects]
public class TaskEditor : Editor
{
#pragma warning disable 0219, 414
	private Task _sTask;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sTask = this.target as Task;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();

		if (GUILayout.Button("Reset State"))
		{
			this._sTask.ResetState();
		}

		if (GUILayout.Button("Initialize"))
		{
			this._sTask.Initialize();
		}

		if (GUILayout.Button("Reset/Init"))
		{
			this._sTask.ResetState();
			this._sTask.Initialize();
		}
	}
}
#endif
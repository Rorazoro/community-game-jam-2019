﻿/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class SceneController : MonoBehaviourSingleton<SceneController>
{
	[SerializeField] private UnityEventFloat _onLoadingSceneAsync;
	public UnityEventFloat _OnLoadingSceneAsync { get { return this._onLoadingSceneAsync; } }

	private IEnumerator LoadSceneAsyncProcess(int sceneBuildIndex, LoadSceneMode loadSceneMode, UnityEventFloat onLoadingSceneAsync)
	{
		AsyncOperation sceneLoadingAsyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneMode);

		while (!sceneLoadingAsyncOperation.isDone)
		{
			this._onLoadingSceneAsync.Invoke(sceneLoadingAsyncOperation.progress);
			onLoadingSceneAsync?.Invoke(sceneLoadingAsyncOperation.progress);

			yield return null;
		}
	}

	private Coroutine _loadSceneAsyncProcess;

	public void LoadSceneAsync(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single, UnityEventFloat onLoadingSceneAsync = null)
	{
		if (this._loadSceneAsyncProcess != null)
			this.StopCoroutine(this._loadSceneAsyncProcess);

		this._loadSceneAsyncProcess = this.StartCoroutine(this.LoadSceneAsyncProcess(sceneBuildIndex, loadSceneMode, onLoadingSceneAsync));
	}

	#region Next
	public void LoadNextScene(LoadSceneMode loadSceneMode)
	{
		int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

		if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(nextSceneBuildIndex, loadSceneMode);
		}
	}
	public void LoadNextScene() { this.LoadNextScene(LoadSceneMode.Single); }

	//! Async
	public void LoadNextSceneAsync(LoadSceneMode loadSceneMode)
	{
		int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

		if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
		{
			this.LoadSceneAsync(nextSceneBuildIndex, loadSceneMode);
		}
	}
	public void LoadNextSceneAsync() { this.LoadNextSceneAsync(LoadSceneMode.Single); }
	#endregion

	#region Previous
	public void LoadPreviousScene(LoadSceneMode loadSceneMode)
	{
		int previousSceneBuildIndex = SceneManager.GetActiveScene().buildIndex - 1;

		if (previousSceneBuildIndex >= 0)
		{
			SceneManager.LoadScene(previousSceneBuildIndex, loadSceneMode);
		}
	}
	public void LoadPreviousScene() { this.LoadPreviousScene(LoadSceneMode.Single); }

	//! Async
	public void LoadPreviousSceneAsync(LoadSceneMode loadSceneMode)
	{
		int previousSceneBuildIndex = SceneManager.GetActiveScene().buildIndex - 1;

		if (previousSceneBuildIndex >= 0)
		{
			this.LoadSceneAsync(previousSceneBuildIndex, loadSceneMode);
		}
	}
	public void LoadPreviousSceneAsync() { this.LoadPreviousSceneAsync(LoadSceneMode.Single); }
	#endregion

	public void Exit()
	{
		Application.Quit();
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

[System.Serializable]
public class UnityEventFloat : UnityEvent<float> { }

[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> { }

[System.Serializable]
public class UnityEventVector3 : UnityEvent<Vector3> { }

[System.Serializable]
public class UnityEventCollider : UnityEvent<Collider> { }

[System.Serializable]
public class UnityEventCollider2D : UnityEvent<Collider2D> { }

[System.Serializable]
public class UnityEventCallbackContext : UnityEvent<InputAction.CallbackContext> { }

#if UNITY_EDITOR
[CustomEditor(typeof(SceneController))]
[CanEditMultipleObjects]
public class SceneControllerEditor : Editor
{
#pragma warning disable 0219, 414
	private SceneController _sSceneController;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sSceneController = this.target as SceneController;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
﻿/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using TMPro;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	[Header("Automatic Events")]

	[Tooltip("Called on Start when everything is initialized.")]
	[SerializeField] private UnityEvent _onGameAwake;
	public UnityEvent _OnGameAwake { get { return this._OnGameAwake; } }

	[Header("Manual Events")]

	[SerializeField] private UnityEvent _onStartGame;
	public UnityEvent _OnStartGame { get { return this._onStartGame; } }

	public void StartGame()
	{
		this._onStartGame.Invoke();
	}

	[SerializeField] private UnityEvent _onEndGame;
	public UnityEvent _OnEndGame { get { return this._onEndGame; } }

	public void EndGame()
	{
		this._onEndGame.Invoke();
	}

	public void RestartGame()
	{
		this.EndGame();
		this.StartGame();
	}

	private IEnumerator RestartGameProcess(float delay)
	{
		this.EndGame();

		yield return new WaitForSecondsRealtime(delay);

		this.StartGame();
	}

	public void RestartGame(float delay)
	{
		this.StartCoroutine(this.RestartGameProcess(delay));
	}

	private void Start()
	{
		this._onGameAwake.Invoke();
	}
	
#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects]
public class GameManagerEditor : Editor
{
#pragma warning disable 0219, 414
	private GameManager _sGameManager;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sGameManager = this.target as GameManager;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
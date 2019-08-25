/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Reaction
{
	[SerializeField] private UnityEvent _onReact;
	public UnityEvent _OnReact { get { return this._onReact; } }

	public void React()
	{
		this._onReact.Invoke();
	}
}
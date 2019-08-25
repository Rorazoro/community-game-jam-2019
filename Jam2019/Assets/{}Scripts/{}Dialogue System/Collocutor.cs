/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "name Collocutor", menuName = "Storytelling/Collocutor")]
public class Collocutor : ScriptableObject
{
	[SerializeField] private string _profileName;
	public string _ProfileName { get { return this._profileName; } }

	[SerializeField] private Sprite _profileIcon;
	public Sprite _ProfileIcon { get { return this._profileIcon; } }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Collocutor))]
[CanEditMultipleObjects]
public class CollocutorEditor : Editor
{
#pragma warning disable 0219
	private Collocutor _sCollocutor;
#pragma warning restore 0219

	private void OnEnable()
	{
		this._sCollocutor = this.target as Collocutor;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
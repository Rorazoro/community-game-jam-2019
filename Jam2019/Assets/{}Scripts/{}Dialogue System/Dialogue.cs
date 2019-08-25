/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "name Dialogue", menuName = "Storytelling/Dialogue")]
public class Dialogue : ScriptableObject
{
	[SerializeField] private List<DialogueData> _dialogueData;
	public List<DialogueData> _DialogueData { get { return this._dialogueData; } }
}

[System.Serializable]
public class DialogueData
{
	[TextArea(1, 100)]
	[SerializeField] private string _sentenceText;
	public string _SentenceText { get { return this._sentenceText; } }

	[SerializeField] private AudioClip _audioCover;
	public AudioClip _AudioCover { get { return this._audioCover; } }

	[SerializeField] private Collocutor _collocutor;
	public Collocutor _Collocutor { get { return this._collocutor; } }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Dialogue))]
[CanEditMultipleObjects]
public class DialogueEditor : Editor
{
#pragma warning disable 0219
	private Dialogue _sDialogue;
#pragma warning restore 0219

	private void OnEnable()
	{
		this._sDialogue = this.target as Dialogue;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
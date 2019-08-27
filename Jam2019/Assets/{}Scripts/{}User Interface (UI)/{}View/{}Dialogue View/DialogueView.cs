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

public class DialogueView : View
{
	[SerializeField] private TextMeshProUGUI _messageTextField;
	public TextMeshProUGUI _MessageTextField { get { return this._messageTextField; } }

	[SerializeField] private TextMeshProUGUI _collocutorNameTextField;
	public TextMeshProUGUI _CollocutorNameTextField { get { return this._collocutorNameTextField; } }

	[SerializeField] private Image _collocutorIconImageField;
	public Image _CollocutorIconImageField { get { return this._collocutorIconImageField; } }

	private Dialogue _displayedDialogue;
	private int _displayDialogueDataIndex;

	private void DisplayDialogueData(DialogueData dialogueData)
	{
		//AudioPlayer.Instance.PlaySFX(dialogueData._AudioCover);

		this._messageTextField.text = dialogueData._SentenceText;

		this._collocutorNameTextField.text = dialogueData._Collocutor._ProfileName;
		this._collocutorIconImageField.sprite = dialogueData._Collocutor._ProfileIcon;
	}

	public virtual void DisplayDialogue(Dialogue dialogue)
	{
		this._displayedDialogue = dialogue;

		this._displayDialogueDataIndex = -1;

		this.DisplayNext();
	}

	public void DisplayNext()
	{
		this._displayDialogueDataIndex++;

		if (this._displayDialogueDataIndex < this._displayedDialogue._DialogueData.Count)
			this.DisplayDialogueData(this._displayedDialogue._DialogueData[this._displayDialogueDataIndex]);
		else
			this.Hide();
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueView))]
[CanEditMultipleObjects]
public class DialogueViewEditor : ViewEditor
{
#pragma warning disable 0219, 414
	private DialogueView _sDialogueView;
#pragma warning restore 0219, 414

	protected override void OnEnable()
	{
		base.OnEnable();

		this._sDialogueView = this.target as DialogueView;
	}
}
#endif
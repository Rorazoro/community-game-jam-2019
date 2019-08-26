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

public class ImageToggle : MonoBehaviour
{
	[SerializeField] private Image _image;

	[SerializeField] private Sprite _activeSprite;
	[SerializeField] private Sprite _inactiveSprite;

	[SerializeField] private bool _active;

	public void Toggle(bool state)
	{
		this._active = state;

		this._image.overrideSprite = this._active ? this._activeSprite : this._inactiveSprite;
	}

	public void Toggle() => this.Toggle(!this._active);

	private void Awake()
	{
		this.Toggle(this._active);
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._image = this.GetComponent<Image>();
	}

	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ImageToggle))]
[CanEditMultipleObjects]
public class ImageToggleEditor : Editor
{
#pragma warning disable 0219, 414
	private ImageToggle _sImageToggle;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sImageToggle = this.target as ImageToggle;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif
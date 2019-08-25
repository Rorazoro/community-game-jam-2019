/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AdvancedButton : Button
{
	[SerializeField] private UnityEvent _onPointerEnter;
	public UnityEvent _OnPointerEnter { get { return this._onPointerEnter; } }

	[SerializeField] private UnityEvent _onPointerExit;
	public UnityEvent _OnPointerExit { get { return this._onPointerExit; } }

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);

		this._onPointerEnter.Invoke();
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);

		this._onPointerExit.Invoke();
	}

	[SerializeField] private UnityEvent _onPointerDown;
	public UnityEvent _OnPointerDown { get { return this._onPointerDown; } }

	//[FormerlySerializedAs("_onPointerUp")]
	[SerializeField] private UnityEvent _onPointerUp;
	public UnityEvent _OnPointerUp { get { return this._onPointerUp; } }

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		//UISystemProfilerApi.AddMarker("Button._onPointerDown", this);

		this._onPointerDown.Invoke();
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);

		//UISystemProfilerApi.AddMarker("Button._onPointerUp", this);

		this._onPointerUp.Invoke();
	}

	[SerializeField] private UnityEvent _onSelect;
	public UnityEvent _OnSelect { get { return this._onSelect; } }

	[SerializeField] private UnityEvent _onDeselect;
	public UnityEvent _OnDeselect { get { return this._onDeselect; } }

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);

		this._onSelect.Invoke();
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);

		this._onDeselect.Invoke();
	}
}
﻿/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName = "name Inventory", menuName = "Inventory/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
	private const int DEFAULT_SLOT_INDEX = -1;

	private InventoryView _inventoryDisplayController;
	public InventoryView InventoryDisplayController__ { set { this._inventoryDisplayController = value; } }

	private InventoryItemSlot[] _itemSlots;
	private int _itemsQuantity;

	[SerializeField] private int _size = 30;
	public int _Size { get { return this._size; } }

	[SerializeField] private int _capacity = 20;
	public int _Capacity { get { return this._capacity; } }

	public void Initialize(int size, int capacity)
	{
		this._itemSlots = new InventoryItemSlot[size];

		this._itemsQuantity = 0;

		if (capacity <= size)
			this._capacity = capacity;
		else
		{
			Debug.LogError(this.GetType().Name + " - Capacity can't be larger than size. Capacity was set to max value possible = size.");
			this._capacity = size;
		}

		this._size = this._itemSlots.Length;
	}

	public void Initialize()
	{
		this.Initialize(this._size, this._capacity);
	}

	public Inventory(int size, int capacity)
	{
		this.Initialize(size, capacity);
	}

	/// <summary>
	/// Factory method.
	/// </summary>
	/// <param name="size"></param>
	/// <param name="capacity"></param>
	/// <returns></returns>
	public static Inventory CreateInstance(int size, int capacity)
	{
		Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
		inventory.Initialize(size, capacity);

		return inventory;
	}

	public void AddItemDisplay(InventoryItemSlotView inventoryItemSlotView, int index)
	{
		inventoryItemSlotView.Clear(InventoryView.ClearMode.Partial);
		this._itemSlots[index].InventoryItemSlotView__ = inventoryItemSlotView;
	}

	public event System.Action<InventoryItemSharedData, bool> OnItemAddition;

	public bool Add(InventoryItem item)
	{
		if (item.InventoryItemSharedData_._Stackable)
		{
			int firstFreeSlotIndex = Inventory.DEFAULT_SLOT_INDEX;

			for (int i = 0; i < this._capacity; i++)
			{
				if (this._itemSlots[i]._IsFree)
				{
					if (firstFreeSlotIndex == Inventory.DEFAULT_SLOT_INDEX)
						firstFreeSlotIndex = i;
				}
				else if (this._itemSlots[i].Item.InventoryItemSharedData_.Id == item.InventoryItemSharedData_.Id)
				{
					this._itemSlots[i].Item._Quantity += item._Quantity;

					this.OnItemAddition?.Invoke(item.InventoryItemSharedData_, true);
					return true;
				}
			}

			if (firstFreeSlotIndex != Inventory.DEFAULT_SLOT_INDEX)
			{
				this._itemSlots[firstFreeSlotIndex].Item = item;

				this._itemsQuantity++;

				this.OnItemAddition?.Invoke(item.InventoryItemSharedData_, true);
				return true;
			}
		}
		else
		{
			if (this._itemsQuantity == this._capacity)
			{
				this.OnItemAddition?.Invoke(item.InventoryItemSharedData_, false);
				return false;
			}

			for (int i = 0; i < this._capacity; i++)
			{
				if (this._itemSlots[i]._IsFree)
				{
					this._itemSlots[i].Item = item;

					this._itemsQuantity++;

					this.OnItemAddition?.Invoke(item.InventoryItemSharedData_, true);
					return true;
				}
			}
		}

		this.OnItemAddition?.Invoke(item.InventoryItemSharedData_, false);
		return false;
	}

	public bool Add(InventoryItemSharedData inventoryItemSharedData)
	{
		return this.Add(new InventoryItem(inventoryItemSharedData.Id));
	}

	public bool Add(InventoryItemSharedData inventoryItemSharedData, int quantity)
	{
		return this.Add(new InventoryItem(inventoryItemSharedData.Id, quantity));
	}

	public bool Remove(InventoryItem item)
	{
		if (this._itemsQuantity == 0)
			return false;

		for (int i = 0; i < this._capacity; i++)
		{
			if (this._itemSlots[i]._IsFree)
				continue;

			if (this._itemSlots[i].Item == item)
			{
				this._itemSlots[i].Clear(InventoryView.ClearMode.Partial);

				this._itemsQuantity--;
				return true;
			}
		}

		return false;
	}

	public bool Remove(int itemId)
	{
		if (this._itemsQuantity == 0)
			return false;

		for (int i = 0; i < this._capacity; i++)
		{
			if (this._itemSlots[i]._IsFree)
				continue;

			if (this._itemSlots[i].Item.InventoryItemSharedData_.Id == itemId)
			{
				this._itemSlots[i].Clear(InventoryView.ClearMode.Partial);

				this._itemsQuantity--;
				return true;
			}
		}

		return false;
	}

	public bool RemoveAt(int index)
	{
		if (this._itemSlots[index].Item != null)
			return this.Remove(this._itemSlots[index].Item);

		return false;
	}

	public enum RemoveResultStatus { NegativeQuantity, Success, NotFound }

	public RemoveResultStatus Remove(int itemId, int quantity)
	{
		if (this._itemsQuantity == 0)
			return RemoveResultStatus.NotFound;

		for (int i = 0; i < this._capacity; i++)
		{
			if (this._itemSlots[i]._IsFree)
				continue;

			if (this._itemSlots[i].Item.InventoryItemSharedData_.Id == itemId)
			{
				if (this._itemSlots[i].Item._Quantity < quantity)
					return RemoveResultStatus.NegativeQuantity;

				this._itemSlots[i].Item._Quantity -= quantity;

				if (this._itemSlots[i].Item._Quantity == 0)
					this._itemSlots[i].Clear(InventoryView.ClearMode.Partial);

				this._itemsQuantity--;
				return RemoveResultStatus.Success;
			}
		}

		return RemoveResultStatus.NotFound;
	}

	public bool Contains(InventoryItem item)
	{
		if (this._itemsQuantity == 0)
			return false;

		for (int i = 0; i < this._capacity; i++)
		{
			if (this._itemSlots[i]._IsFree)
				continue;

			if (this._itemSlots[i].Item == item)
				return true;
		}

		return false;
	}

	public bool Contains(int itemId)
	{
		if (this._itemsQuantity == 0)
			return false;

		for (int i = 0; i < this._capacity; i++)
		{
			if (this._itemSlots[i]._IsFree)
				continue;

			if (this._itemSlots[i].Item.InventoryItemSharedData_.Id == itemId)
				return true;
		}

		return false;
	}

	public void Clear()
	{
		this._itemSlots = null;
	}

	public void Rebuild()
	{
		// Inventory code

		if (this._inventoryDisplayController != null)
		{
			// Update display code
		}
	}
}

[System.Serializable]
public struct InventoryItemSlot
{
	private InventoryItemSlotView _itemSlotView;
	public InventoryItemSlotView InventoryItemSlotView__ { set { this._itemSlotView = value; } }

	private InventoryItem _item;
	public InventoryItem Item
	{
		get { return this._item; }
		set
		{
			if (value != null)
			{
				this._item = value;

				if (this._itemSlotView != null)
				{
					this._itemSlotView._ItemView.Open();
					this._itemSlotView._ItemView.Display(this._item);
					this._item.OnQuantityChange += this._itemSlotView._ItemView.UpdateLayout;
				}
			}
#if UNITY_EDITOR
			else
			{
				Debug.LogWarning("Trying to assign null. Use `Clear()` instead.");
			}
#endif
		}
	}

	public bool _IsFree { get { return this._item == null; } }

	public void Clear(InventoryView.ClearMode clearMode)
	{
		this._item = null;
		this._itemSlotView?.Clear(clearMode);
	}
}
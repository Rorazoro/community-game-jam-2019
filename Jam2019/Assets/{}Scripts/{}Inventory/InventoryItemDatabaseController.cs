/* Created by Luna.Ticode */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class InventoryItemDatabaseController : MonoBehaviourSingleton<InventoryItemDatabaseController>
{
	private const int DEFAULT_FREE_ITEM_ID = -1;
	private static int s_freeItemId = DEFAULT_FREE_ITEM_ID;

	private Dictionary<int, InventoryItemSharedData> _itemId_inventoryItemSharedDataRelation;

	[SerializeField] private List<InventoryItemSharedData> _inventoryItemSharedData;

	public InventoryItemSharedData FetchSharedData(int itemId)
	{
		return this._itemId_inventoryItemSharedDataRelation[itemId];
	}

	private void InitializeDatabase()
	{
		InventoryItemDatabaseController.s_freeItemId = DEFAULT_FREE_ITEM_ID;

		this._itemId_inventoryItemSharedDataRelation = new Dictionary<int, InventoryItemSharedData>(this._inventoryItemSharedData.Count);

		for (int i = 0; i < this._inventoryItemSharedData.Count; i++)
		{
			// Index ItemSharedData
			this._inventoryItemSharedData[i].Id = ++InventoryItemDatabaseController.s_freeItemId;

			this._itemId_inventoryItemSharedDataRelation.Add(this._inventoryItemSharedData[i].Id, this._inventoryItemSharedData[i]);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		this.InitializeDatabase();
	}

#if UNITY_EDITOR
	[Header("Editor Only")]

	[SerializeField] private string[] _itemsSearchFoldersPaths = new string[]
	{
		"Assets/_Specific/#Inventory/{#}Items"
	};

	public void LoadItemsDataFromFoldersEO()
	{
		string[] assetsGUIDs = AssetDatabase.FindAssets("t: InventoryItemSharedData", this._itemsSearchFoldersPaths);
		
		for (int a = 0; a < assetsGUIDs.Length; a++)
		{
			this._inventoryItemSharedData.Add(
				AssetDatabase.LoadAssetAtPath<InventoryItemSharedData>(AssetDatabase.GUIDToAssetPath(assetsGUIDs[a]))
			);
		}
	}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(InventoryItemDatabaseController))]
[CanEditMultipleObjects]
public class InventoryItemDatabaseControllerEditor : Editor
{
	protected InventoryItemDatabaseController sInventoryItemDatabaseController;

	private void OnEnable()
	{
#pragma warning disable 0219
		this.sInventoryItemDatabaseController = this.target as InventoryItemDatabaseController;
#pragma warning restore 0219
	}

	public override void OnInspectorGUI()
	{
		this.serializedObject.Update();

		this.DrawDefaultInspector();

		if (GUILayout.Button("Load Items Data From Folders"))
		{
			this.sInventoryItemDatabaseController.LoadItemsDataFromFoldersEO();

			EditorUtility.SetDirty(this.sInventoryItemDatabaseController);
		}

		this.serializedObject.ApplyModifiedProperties();
	}
}
#endif
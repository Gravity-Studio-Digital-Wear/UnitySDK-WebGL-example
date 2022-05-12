using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEntity;
using GravityLayer;
using GravityLayer.Wearables;

[RequireComponent(typeof(ItemContainer))]
public class CustomizedInventory : MonoBehaviour
{
    [SerializeField] private Item _baseWear;
    [SerializeField] private GLayerManager _gtsManager;
    [SerializeField] private GameObject _baseAvatar;
    private ItemContainer _inventory;
    private bool _gtsWearablesFetched;

    void Awake()
    {
        _inventory = GetComponent<ItemContainer>();
        _inventory.OnInventoryOpen += FetchGLayerWearables;
        _gtsWearablesFetched = false;
    }

    void Start()
    {
        ItemWearable itemWearable;
        itemWearable = (ItemWearable)ScriptableObject.CreateInstance("ItemWearable");
        itemWearable.type = ItemType.ToolOrWeapon;
        itemWearable.itemName = _baseWear.itemName;
        itemWearable.itemPerSlot = 1;
        itemWearable.icon = _baseWear.icon;
        itemWearable.Avatar = _baseAvatar;
        _inventory.AddItem(itemWearable);
    }

    async void FetchGLayerWearables()
    {
        if (_gtsWearablesFetched) return;

        if (!_gtsManager.GrLConnection.ConnectionEstablished)
        {
            await _gtsManager.EstablishConnection();
        }

        ItemWearable itemWearable;

        await _gtsManager.Wardrobe.FetchWearables();

        foreach (WearableBase w in _gtsManager.Wardrobe.Wearables)
        {
            itemWearable = (ItemWearable) ScriptableObject.CreateInstance("ItemWearable");
            itemWearable.type = ItemType.ToolOrWeapon;
            itemWearable.itemName = w.Title;
            itemWearable.itemPerSlot = 1;
            itemWearable.icon = Sprite.Create(w.PreviewImage, new Rect(0, 0, w.PreviewImage.width, w.PreviewImage.height), new Vector2(0.5f, 0.5f));
            itemWearable.wearable = w;
            _inventory.AddItem(itemWearable);
        }

        _gtsWearablesFetched = true;
    }
}

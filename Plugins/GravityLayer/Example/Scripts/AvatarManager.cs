using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEntity;

public class AvatarManager : MonoBehaviour
{
    public GameObject Avatar { get; private set; }

    [SerializeField] private GameObject _avatarHolder;
    [SerializeField] private GameObject _baseAvatar;
    [SerializeField] private Text _loadingAvatarText;

    void Start()
    {
        Avatar = _baseAvatar;
        ItemManager.OnItemEquip += HandleItemEquip;
        _loadingAvatarText.gameObject.SetActive(false);
    }

    public void SetDefaultAvatar()
    {
        ChangeAvatar(_baseAvatar);
    }

    void ChangeAvatar(GameObject newAvatar)
    {
        Avatar.SetActive(false);
        newAvatar.transform.position = Avatar.transform.position;
        newAvatar.transform.rotation = Avatar.transform.rotation;
        newAvatar.transform.SetParent(_avatarHolder.transform);
        newAvatar.SetActive(true);
        Avatar = newAvatar;
    }

    async void HandleItemEquip(Item item)
    {
        if (item is ItemWearable)
        {
            ItemWearable w = (ItemWearable)item;
            _loadingAvatarText.gameObject.SetActive(true);
            await w.DownloadAvatar();
            _loadingAvatarText.gameObject.SetActive(false);
            ChangeAvatar(w.Avatar);
        }
    }
}

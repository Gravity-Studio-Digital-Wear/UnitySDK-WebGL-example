using System;
using XEntity;
using GravityTS.Wearables;
using UnityEngine;
using System.Threading.Tasks;
using Siccity.GLTFUtility;
using GravityTS.MetaversePreparation.RPM;

[CreateAssetMenu(fileName = "New Item", menuName = "XEntity/ItemWearable")]
public class ItemWearable : Item
{
    public WearableBase wearable;

    public GameObject Avatar;

    public async Task DownloadAvatar()
    {
        // if Avatar GameObject exists no need to download it again
        if (Avatar != null) return;

        await wearable.DownloadAvatar();
        Debug.Log("Array size " + wearable.AvatarBytes.Length.ToString());

        Avatar = Importer.LoadFromBytes(wearable.AvatarBytes);
        PostProcessing.PrepareAvatar(Avatar);
    }
}

using XEntity;
using GravityLayer.Wearables;
using UnityEngine;
using System.Threading.Tasks;
using Siccity.GLTFUtility;
using GravityLayer.MetaversePreparation.RPM;
using GravityLayer.Utils;

[CreateAssetMenu(fileName = "New Item", menuName = "XEntity/ItemWearable")]
public class ItemWearable : Item
{
    public WearableBase wearable;

    public GameObject Avatar;

    public async Task DownloadAvatar()
    {
        // if Avatar GameObject exists no need to download it again
        if (Avatar != null) return;

        await Downloader.DownloadAvatar(wearable.ModelUrl, HandleDownloadAvatarResponse);
    }

    void HandleDownloadAvatarResponse(byte[] avatarBytes)
    {
        Avatar = Importer.LoadFromBytes(avatarBytes);
        Debug.Log("Array size " + avatarBytes.Length.ToString());

        PostProcessing.PrepareAvatar(Avatar);
    }
}

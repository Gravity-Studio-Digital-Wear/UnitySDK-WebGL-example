using System;
using UnityEngine;
using GravityLayer.Utils;
using System.Threading.Tasks;

namespace GravityLayer.Wearables
{
    public class WearableBase
    {
        public static Action<WearableBase> OnDownloadAvatarFinished;

        public string Title { get; private set; }
        public Texture2D PreviewImage { get; private set; }
        public string ModelUrl { get; private set; }

        public byte[] AvatarBytes;

        public WearableBase(string title, Texture2D preview, string modelUrl)
        {
            Title = title;
            PreviewImage = preview;
            ModelUrl = modelUrl;
        }

        public async Task DownloadAvatar()
        {
            if (AvatarBytes == null || AvatarBytes.Length == 0)
            {
                await Downloader.DownloadAvatar(ModelUrl, HandleDownloadAvatarResponse);
            }

            OnDownloadAvatarFinished?.Invoke(this);
        }

        void HandleDownloadAvatarResponse(byte[] avatarBytes)
        {
            AvatarBytes = avatarBytes;
        }
    }
}
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siccity.GLTFUtility;
using GravityTS.Utils;
using GravityTS.Utils.RPM;
using System.Threading.Tasks;

namespace GravityTS.Wearables
{
    public class Wearable
    {
        public static Action<GameObject> OnDownloadAvatarFinished;

        public string Title { get; private set; }
        public Texture2D PreviewImage { get; private set; }
        public string ModelUrl { get; private set; }
        public GameObject Avatar { get; private set; }

        public Wearable(string title, Texture2D preview, string modelUrl)
        {
            Title = title;
            PreviewImage = preview;
            ModelUrl = modelUrl;
        }

        public Wearable(string title, Texture2D preview, string modelUrl, GameObject avatar)
        {
            Title = title;
            PreviewImage = preview;
            ModelUrl = modelUrl;
            Avatar = avatar;
        }

        async public Task DownloadAvatar()
        {
            if (Avatar == null)
            {
                await Downloader.DownloadAvatar(ModelUrl);
                /* TODO delete
                byte[] avatarBytes;
                if (File.Exists(ModelUrl))
                {
                    avatarBytes = File.ReadAllBytes(ModelUrl);
                    Avatar = Importer.LoadFromBytes(avatarBytes);
                }*/
                // TODO check if download successful
                Avatar = Importer.LoadFromBytes(Downloader.AvatarBytes);
                // TODO make postprocessing dependant on metaverse id
                RPMPostProcessing.PrepareAvatar(Avatar);
            }

            OnDownloadAvatarFinished?.Invoke(Avatar);
        }
    }
}
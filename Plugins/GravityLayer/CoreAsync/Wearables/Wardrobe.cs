using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityLayer.Utils;
using System.IO;
using System.Threading.Tasks;

namespace GravityLayer.Wearables
{
    public class Wardrobe : IWardrobe
    {
        public List<IWearable> Wearables { get; private set; }

        private IFetchWearables _connection;
        private string _metaverseId;

        public Wardrobe(IFetchWearables connection, string metaverseId)
        {
            Wearables = new List<IWearable>();
            _connection = connection;
            _metaverseId = metaverseId;
        }

        public async Task FetchWearables()
        {
            string jsonString = await _connection.FetchWearablesByMetaverseId(_metaverseId);
            await FillWardrobeFromJsonString(jsonString);
        }

        async Task FillWardrobeFromJsonString(string jsonString)
        {
            Texture texture;
            Wearables.Clear();
            var jsonData = JsonUtility.FromJson<WardrobeResult>("{\"result\":" + jsonString + "}");
            foreach (var r in jsonData.result)
            {
                // TODO check metaverse id
                if ((r.product.metadata != null) & (r.product.metadata.Length > 0))
                {
                    texture = await Downloader.DownloadImage(r.product.metadata[0].previewImage);
                    Wearables.Add(new WearableBase(r.product.name, (Texture2D)texture, r.product.metadata[0].modelUrl, _metaverseId));
                }
            }
        }
    }
}
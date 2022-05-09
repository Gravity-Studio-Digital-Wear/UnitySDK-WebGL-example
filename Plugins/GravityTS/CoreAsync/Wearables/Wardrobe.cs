using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityTS.Utils;
using System.IO;
using System.Threading.Tasks;

namespace GravityTS.Wearables
{
    public class Wardrobe
    {
        public List<WearableBase> Wearables { get; private set; }

        private IFetchWearables _connection;

        public Wardrobe(IFetchWearables connection)
        {
            Wearables = new List<WearableBase>();
            _connection = connection;
        }

        public async Task FetchWearables()
        {
            string jsonString = await _connection.FetchWearables();
            await FillWardrobeFromJsonString(jsonString);
        }

        async Task FillWardrobeFromJsonString(string jsonString)
        {
            Wearables.Clear();
            var jsonData = JsonUtility.FromJson<WardrobeResult>("{\"result\":" + jsonString + "}");
            foreach (var r in jsonData.result)
            {
                // TODO check metaverse id
                if ((r.product.metadata != null) & (r.product.metadata.Length > 0))
                {
                    await Downloader.DownloadImage(r.product.metadata[0].previewImage);
                    Wearables.Add(new WearableBase(r.product.name, (Texture2D)Downloader.Texture, r.product.metadata[0].modelUrl));
                }
            }
        }
    }
}
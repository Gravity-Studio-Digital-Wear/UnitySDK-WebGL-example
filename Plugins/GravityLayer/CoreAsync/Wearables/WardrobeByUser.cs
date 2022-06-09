using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using GravityLayer.Utils;

namespace GravityLayer.Wearables
{
    class WardrobeByUser : IWardrobe
    {
        public List<IWearable> Wearables { get; private set; }

        private IFetchWearablesByAddress _APIWrapper;

        public WardrobeByUser(IFetchWearablesByAddress APIWrapper)
        {
            Wearables = new List<IWearable>();
            _APIWrapper = APIWrapper;
        }

        public async Task FetchInteroperableWearables(string address)
        {
            string jsonString = await _APIWrapper.GetNFTsByAddress(address);
            await FillWardrobeFromJsonString(jsonString);
        }

        async Task FillWardrobeFromJsonString(string jsonString)
        {
            Texture texture;
            Wearables.Clear();
            var jsonData = JsonUtility.FromJson<WardrobeResult>("{\"result\":" + jsonString + "}");
            foreach (var r in jsonData.result)
            {
                if ((r.product.metadata != null) & (r.product.metadata.Length > 0))
                {
                    texture = await Downloader.DownloadImage(r.product.metadata[0].previewImage);
                    Wearables.Add(new WearableBase(r.product.name, (Texture2D)texture, r.product.metadata[0].modelUrl));
                }
            }
        }
    }
}

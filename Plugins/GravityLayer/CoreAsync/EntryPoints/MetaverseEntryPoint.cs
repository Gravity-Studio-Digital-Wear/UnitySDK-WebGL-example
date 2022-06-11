using System;
using GravityLayer.Wearables;
using GravityLayer.Utils;
using System.Threading.Tasks;

namespace GravityLayer
{
    public class MetaverseEntryPoint
    {
        public MetaverseAPIWrapper GLMetaverseAPIWrapper { get; private set; }
        public WardrobeByUser Wardrobe { get; private set; }

        public MetaverseEntryPoint(string apiUrl, string secret)
        {
            GLMetaverseAPIWrapper = new MetaverseAPIWrapper(apiUrl, secret);
            Wardrobe = new WardrobeByUser(GLMetaverseAPIWrapper);
        }
    }
}

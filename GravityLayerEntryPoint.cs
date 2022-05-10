using System;
using GravityLayer.Wearables;
using GravityLayer.Utils;
using System.Threading.Tasks;

namespace GravityLayer
{
    public class GravityLayerEntryPoint
    {
        public Connection GrLConnection { get; private set; }
        public Wardrobe Wardrobe { get; private set; }

        private string _apiUrl;

        private string _account;

        public GravityLayerEntryPoint(string apiUrl, string account, Func<string, Task<string>> signMessage)
        {
            _apiUrl = apiUrl;
            _account = account;
 
            GrLConnection = new Connection(_apiUrl, _account, signMessage);
            Wardrobe = new Wardrobe(GrLConnection);
        }
    }
}
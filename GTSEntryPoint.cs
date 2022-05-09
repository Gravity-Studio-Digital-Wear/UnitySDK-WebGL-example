using System;
using GravityTS.Wearables;
using GravityTS.Utils;
using System.Threading.Tasks;

namespace GravityTS
{
    public class GTSEntryPoint
    {
        public Connection GTSConnection { get; private set; }
        public Wardrobe Wardrobe { get; private set; }

        private string _apiUrl;

        private string _account;

        public GTSEntryPoint(string apiUrl, string account, Func<string, Task<string>> signMessage)
        {
            _apiUrl = apiUrl;
            _account = account;
 
            GTSConnection = new Connection(_apiUrl, _account, signMessage);
            Wardrobe = new Wardrobe(GTSConnection);
        }
    }
}
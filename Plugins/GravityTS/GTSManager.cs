using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityTS.Wearables;
using GravityTS.Utils;
using System.Threading.Tasks;

namespace GravityTS
{
    public class GTSManager : MonoBehaviour
    {
        public Connection GTSConnection { get; private set; }
        public Wardrobe Wardrobe { get; private set; }

        [SerializeField] private string _apiUrl = "https://gravity-dev.easychain.dev/api";

        private string _account;

        void Awake()
        {
            // Wallet address
            _account = PlayerPrefs.GetString("Account");

            GTSConnection = new Connection(_apiUrl, _account);
            Wardrobe = new Wardrobe(GTSConnection);
        }

        public virtual async Task EstablishConnection()
        {
            await GTSConnection.EstablishConnection();
        }
    }
}
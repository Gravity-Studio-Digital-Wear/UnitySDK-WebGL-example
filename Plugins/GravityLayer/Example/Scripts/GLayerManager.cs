using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityLayer;
using GravityLayer.Wearables;
using GravityLayer.Utils;
using System.Threading.Tasks;

public class GLayerManager : MonoBehaviour
{
    public GravityLayerEntryPoint GLayerEntryPoint;
    public Connection GLayerConnection;
    public Wardrobe Wardrobe;

    [SerializeField] protected string _apiUrl = "https://gravity-dev.easychain.dev/api";
    [SerializeField] protected string _metaverseId = "ReadyPlayerMe";

    private string _account;

    void Awake()
    {
        // Wallet address
        _account = PlayerPrefs.GetString("Account");

        GLayerEntryPoint = new GravityLayerEntryPoint(_apiUrl, _account, _metaverseId, Web3GL.Sign);

        GLayerConnection = GLayerEntryPoint.GLayerConnection;
        Wardrobe = GLayerEntryPoint.Wardrobe;
    }

    public virtual async Task EstablishConnection()
    {
        await GLayerConnection.EstablishConnection();
    }
}
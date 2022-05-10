using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityLayer;
using GravityLayer.Wearables;
using GravityLayer.Utils;
using System.Threading.Tasks;

public class GLayerManager : MonoBehaviour
{
    public GravityLayerEntryPoint GrLEntryPoint;
    public Connection GrLConnection;
    public Wardrobe Wardrobe;

    [SerializeField] protected string _apiUrl = "https://gravity-dev.easychain.dev/api";

    private string _account;

    void Awake()
    {
        // Wallet address
        _account = PlayerPrefs.GetString("Account");

        GrLEntryPoint = new GravityLayerEntryPoint(_apiUrl, _account, Web3GL.Sign);

        GrLConnection = GrLEntryPoint.GrLConnection;
        Wardrobe = GrLEntryPoint.Wardrobe;
    }

    public virtual async Task EstablishConnection()
    {
        await GrLConnection.EstablishConnection();
    }
}
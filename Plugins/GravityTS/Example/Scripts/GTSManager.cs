using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GravityTS;
using GravityTS.Wearables;
using GravityTS.Utils;
using System.Threading.Tasks;

public class GTSManager : MonoBehaviour
{
    public GTSEntryPoint GTSEntryPoint;
    public Connection GTSConnection;
    public Wardrobe Wardrobe;

    [SerializeField] protected string _apiUrl = "https://gravity-dev.easychain.dev/api";

    private string _account;

    void Awake()
    {
        // Wallet address
        _account = PlayerPrefs.GetString("Account");

        GTSEntryPoint = new GTSEntryPoint(_apiUrl, _account, Web3GL.Sign);

        GTSConnection = GTSEntryPoint.GTSConnection;
        Wardrobe = GTSEntryPoint.Wardrobe;
    }

    public virtual async Task EstablishConnection()
    {
        await GTSConnection.EstablishConnection();
    }
}
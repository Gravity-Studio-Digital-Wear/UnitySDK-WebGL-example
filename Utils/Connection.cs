using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace GravityLayer.Utils
{
    public interface IFetchWearables
    {
        public Task<string> FetchWearablesByMetaverseId(string metaverseId);
    }

    public class Connection : IFetchWearables
    {
        public bool ConnectionEstablished { get; private set; }

        private string _address;
        private string _uuid;
        private string _signature;
        private string _jwt;
        private string _apiUrl;
        private Func<string, Task<string>> _signMessage;

        public Connection(string apiUrl, string address, Func<string, Task<string>> signMessage)
        {
            _apiUrl = apiUrl;
            _address = address;
            _signMessage = signMessage;
            _jwt = null;
            ConnectionEstablished = false;
        }

        public async Task<string> EstablishConnection()
        {
            await Challenge();
            await Sign(_uuid);
            await AuthLogin();
            if (_jwt != null)
            {
                ConnectionEstablished = true;
                Debug.Log("Connection to Gravity API established");
            }
            else
            {
                Debug.Log("Connection to Gravity API has not been established");
            }
            return _jwt;
        }

        public async Task Challenge()
        {
            string url = _apiUrl + "/auth/challenge";
            string json = "{\"address\": \"" + _address + "\"}";
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            try
            {
                var request = UnityWebRequest.Post(url, "POST");
                request.SetRequestHeader("Content-Type", "application/json");
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                await OnChallengeResponse(request);
            }
            catch (Exception e) { Debug.Log("ERROR : " + e.Message); }
        }

        IEnumerator OnChallengeResponse(UnityWebRequest req)
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Network error has occured: " + req.GetResponseHeader(""));
            }
            else
            {
                LoginResponse data = JsonUtility.FromJson<LoginResponse>(req.downloadHandler.text);
                _uuid = data.challenge;
            }

            req.Dispose();
        }

        public async Task Sign(string message)
        {
            try
            {
                _signature = await _signMessage(message);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async Task AuthLogin()
        {
            string url = _apiUrl + "/auth/login";
            string json = "{\"address\": \"" + _address + "\", \"signature\": \"" + _signature + "\"}";
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            try
            {
                var request = UnityWebRequest.Post(url, "POST");
                request.SetRequestHeader("Content-Type", "application/json");
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                await OnLoginResponse(request);
            }
            catch (Exception e) { Debug.Log("ERROR : " + e.Message); }
        }

        IEnumerator OnLoginResponse(UnityWebRequest req)
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
                Debug.Log("Network error has occured: " + req.GetResponseHeader(""));
            LoginResponse data = JsonUtility.FromJson<LoginResponse>(req.downloadHandler.text);
            _jwt = data.token;
            req.Dispose();
        }

        public async Task<string> FetchWearablesByMetaverseId(string metaverseId)
        {
            string url = _apiUrl + "/warehouse/products/my?metaverse=" + metaverseId;

            var result = await HTTPClient.Get(url, _jwt);

            return result;
        }

        public string EstablishConnection(string testToken)
        {
            if (testToken != null)
            {
                _jwt = testToken;
                ConnectionEstablished = true;
                return _jwt;
            }
            else
            {
                ConnectionEstablished = false;
                return null;
            }
        }
    }
}
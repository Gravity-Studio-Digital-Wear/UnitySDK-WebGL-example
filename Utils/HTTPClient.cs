using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GravityTS.Utils
{
    public static class HTTPClient
    {
        public static async Task<string> Get(string url, string jwt)
        {
            try
            {
                using var request = UnityWebRequest.Get(url);

                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + jwt);

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Failed: {request.error}");

                var result = request.downloadHandler.text;

                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"{nameof(Get)} failed: {e.Message}");
                return default;
            }
        }

        public static async Task<string> Get(string url)
        {
            try
            {
                using var request = UnityWebRequest.Get(url);

                request.SetRequestHeader("Content-Type", "application/json");

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Failed: {request.error}");

                var result = request.downloadHandler.text;

                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"{nameof(Get)} failed: {e.Message}");
                return default;
            }
        }
    }
}
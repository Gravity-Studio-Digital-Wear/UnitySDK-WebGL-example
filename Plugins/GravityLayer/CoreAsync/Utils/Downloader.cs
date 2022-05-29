using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace GravityLayer.Utils
{
    public static class Downloader
    {
        public static Texture Texture;

        public static IEnumerator DownloadImage(string mediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);

            yield return request.SendWebRequest();
            if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
                Debug.Log(request.error);
            else
            {
                Texture = DownloadHandlerTexture.GetContent(request);
            }

            request.Dispose();
        }

        public static async Task DownloadAvatar(string url, Action<byte[]> response)
        {
            try
            {
                using var request = new UnityWebRequest(url);

                request.downloadHandler = new DownloadHandlerBuffer();

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Failed: {request.error}");

                response(request.downloadHandler.data);
            }
            catch (Exception e)
            {
                Debug.LogError($"{nameof(DownloadAvatar)} failed: {e.Message}");
            }
        }
    }
}

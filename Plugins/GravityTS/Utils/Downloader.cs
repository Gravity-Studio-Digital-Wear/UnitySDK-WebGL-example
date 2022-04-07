using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GravityTS.Utils
{
    public static class Downloader
    {
        public static Texture Texture;

        public static byte[] AvatarBytes;

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

        public static IEnumerator DownloadAvatar(string url)
        {
            using (UnityWebRequest request = new UnityWebRequest(url))
            {
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
                    Debug.Log(request.error);
                else
                {
                    AvatarBytes = request.downloadHandler.data;
                }
            }
        }
    }
}

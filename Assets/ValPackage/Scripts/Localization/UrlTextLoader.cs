using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ValeryPopov.Common.Localization
{
    /// <summary>
    /// Загружает Текст по url ссылке
    /// </summary>
    public class UrlTextLoader : MonoBehaviour
    {
        public static void LoadText(string url, Action<string> OnLoadedData)
        {
            var loader = new GameObject("Text Loader", typeof(UrlTextLoader)).GetComponent<UrlTextLoader>();
            loader.StartCoroutine(loader.LoadTextEnumeraor(url, OnLoadedData));
        }

        private IEnumerator LoadTextEnumeraor(string url, Action<string> OnLoadedData)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            OnLoadedData?.Invoke(request.downloadHandler.text);
            DestroyImmediate(gameObject);
        }
    }
}
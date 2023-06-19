using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MapClassController
{
    public class Map
    {
        const int MAX_WIDTH = 650;
        const int MAX_HEIGHT = 450;

        #region API
        public static List<Vector2> SetMarker = new List<Vector2>();
        public static float currentLocationLongitude { set; get; }
        public static float currentLocationLatitude { set; get; }
        public static float Longitude { set; get; }
        public static float Latitude { set; get; }
        public static int Size { set; get; }
        public static int Width { set; get; }
        public static int Height { set; get; }
        public static TypeMap SetTypeMap { set; get; }
        public static TypeMapLayer SetTypeMapLayer { set; get; }
        public enum TypeMap
        {
            map,
            sat
        }
        public static bool EnabledLayer { set; get; }
        public enum TypeMapLayer
        {
            skl,
            trf,
        }
        public static Texture GetTexture { get; private set; }
        public static Texture2D GetTexture2D { get; private set; }
        public static void LoadMap()
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StartCoroutine(DownloadMap());
        }
        public static void UpdateLoadMap()
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StopCoroutine(DownloadMap(mono));
            mono.StartCoroutine(DownloadMap(mono));
        }
        public static void UpdateLoadMap(float timeout)
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StopCoroutine(DownloadMap(timeout, mono));
            mono.StartCoroutine(DownloadMap(timeout, mono));
        }
        #endregion

        #region System
        private static string Convert(Vector2 vector)
        {
            string convertX = vector.x.ToString().Replace(',', '.');
            string convertY = vector.y.ToString().Replace(',', '.');

            return convertX + "," + convertY;
        }

        private static string GetUrl()
        {
            var url = "http://static.maps.2gis.com/1.0?" +
                "&zoom=" + Size.ToString() +
                "&size=800x800" +
                "&markers=" + Convert(new Vector2(EventManager.latitude, EventManager.longitude)) + "~"
                + Convert(new Vector2(EventManager.currentLocationLatitude, EventManager.currentLocationLongitude));

            Debug.Log(url);
            return url;
        }
        private static IEnumerator DownloadMap()
        {
            Width = Mathf.Clamp(Width, 0, MAX_WIDTH);
            Height = Mathf.Clamp(Width, 0, MAX_HEIGHT);
            Size = Mathf.Clamp(Size, 0, 17);

            if (Width == 0 | Height == 0)
            {
                Width = MAX_WIDTH;
                Height = MAX_HEIGHT;
            }

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(GetUrl()))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "map_image";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
        }
        private static IEnumerator DownloadMap(MonoBehaviour mono)
        {
            Width = Mathf.Clamp(Width, 0, MAX_WIDTH);
            Height = Mathf.Clamp(Width, 0, MAX_HEIGHT);
            Size = Mathf.Clamp(Size, 0, 17);
            if (Width == 0 | Height == 0)
            {
                Width = MAX_WIDTH;
                Height = MAX_HEIGHT;
            }
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(GetUrl()))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "map_image";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
            yield return new WaitForSeconds(0.075f);
            mono.StartCoroutine(DownloadMap(mono));
        }
        private static IEnumerator DownloadMap(float timeout, MonoBehaviour mono)
        {
            Width = Mathf.Clamp(Width, 0, MAX_WIDTH);
            Height = Mathf.Clamp(Width, 0, MAX_HEIGHT);
            Size = Mathf.Clamp(Size, 0, 17);
            if (Width == 0 | Height == 0)
            {
                Width = MAX_WIDTH;
                Height = MAX_HEIGHT;
            }
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(GetUrl()))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "map_image";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
            yield return new WaitForSeconds(timeout);
            mono.StartCoroutine(DownloadMap(timeout, mono));
        }
        #endregion
    }
}
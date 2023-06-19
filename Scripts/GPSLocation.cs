using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour
{
    [SerializeField] Text gpsTextLog;
    [SerializeField] Text screenSizeText;

    void Start()
    {
        //Debug.Log(Screen.width.ToString() + " || " + Screen.height.ToString());
        screenSizeText.text = $"ScreenSize({Screen.width}, {Screen.height})";
        StartCoroutine(GPSLoc());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GPSLoc() {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        Input.location.Start();

        gpsTextLog.text = "\nНачало проверки";

        if (!Input.location.isEnabledByUser)
        {
            gpsTextLog.text += "\nВключите GPS на телефоне";
            yield return new WaitForSeconds(1f);
        }


        int timeOutLimit = 30;

        while (Input.location.status == LocationServiceStatus.Initializing 
            && timeOutLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            timeOutLimit--;

            gpsTextLog.text += "\nОжидание соединения: " + timeOutLimit.ToString();
        }

        if (timeOutLimit < 1)
        {   
            gpsTextLog.text += "\nПревышен лимит ожидания!";
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            gpsTextLog.text += "\nПроизошла ошибка подключения!";
            yield break;
        }
        else
        {
            gpsTextLog.text += "\nУспех! ";

            InvokeRepeating("UpdateGPSData", .5f, 1f);
        }
    }

    private void UpdateGPSData()
    {

        if (Input.location.status == LocationServiceStatus.Running)
        {
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;

            //EventManager.latitude = lat;
            //EventManager.longitude = lon;

            gpsTextLog.text = $"Lat: {EventManager.latitude},\n" +
                              $"Ton: {EventManager.longitude}";

           
        }
    }
}
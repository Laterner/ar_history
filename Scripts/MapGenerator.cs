using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MapClassController;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject consolePanel;

    [SerializeField] private RawImage image;
    [SerializeField] private Map.TypeMap typeMap;
    [SerializeField] private Map.TypeMapLayer mapLayer;

    [Range(0, 17)]
    [SerializeField] private int sizeZoom = 4;
    [SerializeField] private float latitude;
    [SerializeField] private float longitude;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        LoadMap();
    }

    public void LoadMap()
    {
        if (EventManager.latitude != 0 && EventManager.longitude != 0)
        {
            button.interactable = false;
            Map.EnabledLayer = true;
            Map.Size = sizeZoom;
            Map.SetTypeMap = typeMap;
            Map.SetTypeMapLayer = mapLayer;

            Map.Width = 650;
            Map.Height = 450;

            Map.currentLocationLongitude = EventManager.currentLocationLongitude;
            Map.currentLocationLatitude = EventManager.currentLocationLatitude;
            Map.Longitude = EventManager.longitude;
            Map.Latitude = EventManager.latitude;

            Map.LoadMap();
            StartCoroutine(GetTexture());
        }
        else
        {
            print("Проверка не пройдена!");
        }
    }

    public void ShowConsolePanel()
    {
        consolePanel.SetActive(!consolePanel.activeSelf);
    }

    IEnumerator GetTexture()
    {
        while (Map.GetTexture == null)
        {
            yield return new WaitForSeconds(0.4f);
        }

        image.texture = Map.GetTexture;
        button.interactable = true;
    }

}
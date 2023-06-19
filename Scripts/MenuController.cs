using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class MenuController : MonoBehaviour
{
    public GameObject ARSession;
    public ARTrackedImageManager trackedImageManager;
    public XRReferenceImageLibrary library;

    void Start()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        library = GetComponent<XRReferenceImageLibrary>();
    }

    public void SetGameobjectMask(Locations _loc)
    {
        trackedImageManager.trackedImagePrefab = _loc._prefab;
        EventManager.latitude  = _loc._lat;
        EventManager.longitude = _loc._lon;
        print(_loc._lat);
        print(_loc._lon);
    }
}

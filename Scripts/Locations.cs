using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class Locations : MonoBehaviour
{
    public string _locName = "";
    public float _lat;
    public float _lon;
    public float _size;

    public GameObject _prefab;
    public XRReferenceImageLibrary _library;
}

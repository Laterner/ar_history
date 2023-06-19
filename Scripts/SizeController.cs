using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    [SerializeField] private Text debugText;
    [SerializeField] private Slider slider;

    private GameObject sp = null;

    private string _context(string str1, string str2)
    {
        string str3 = sp == null ? "Null" : "Object Finded";

        return str1 + "\n" + str2 + "\n" + str3;
    }

    void Start()
    {
        debugText.text = _context("str1 ", "str2");
        
    }

    // Update is called once per frame
    void Update()
    {
        float size = slider.value;
        debugText.text = _context("Val: " + slider.value.ToString(), "str2");
        try
        {
            if (sp == null) sp = GameObject.FindGameObjectWithTag("Player");
            else sp.transform.localScale = new Vector3(size, size, size);
        }
        catch {
            print("");
        }
    }
}

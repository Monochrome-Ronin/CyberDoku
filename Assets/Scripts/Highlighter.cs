using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{
    [SerializeField]
    Material _Highlighted;
    [SerializeField]
    Material _Unhighlighted;

    public void Highlight()
    {
        GetComponent<Image>().material = _Highlighted;
    }

    public void Unhighlight()
    {
        GetComponent<Image>().material = _Unhighlighted;
    }
}

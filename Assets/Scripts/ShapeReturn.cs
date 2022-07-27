using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeReturn : MonoBehaviour
{
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && transform.position != startPosition) transform.position = Vector3.Lerp(transform.position, startPosition, 10f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChecker : MonoBehaviour
{
    Transform _transform;
    [SerializeField]
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector3.forward);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "cell")
            {
                print(hit.collider.name);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShape : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D _SellCollider;
    float SellSize;
    RaycastHit2D CurrentHit;
    bool Clicked = false;
    Vector3 startPosition = new Vector3();
    Vector3 offset = new Vector3();
    Vector3 mousePosition;

    void Start()
    {
        SellSize = _SellCollider.size.x;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "shape")
                {
                    startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    offset = hit.transform.parent.transform.position - mousePosition;
                    Clicked = true;
                    CurrentHit = hit;
                    foreach (Transform child in CurrentHit.transform.parent.GetComponentsInChildren<Transform>())
                    {
                        try
                        {
                            child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }           
        }
        if (Input.GetMouseButton(0))
        {
            if (Clicked)
            {
                CurrentHit.transform.parent.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, 0);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Clicked = false;
            foreach (Transform child in CurrentHit.transform.parent.GetComponentsInChildren<Transform>())
            {
                try
                {
                    child.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}

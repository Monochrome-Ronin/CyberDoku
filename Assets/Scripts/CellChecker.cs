using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellChecker : MonoBehaviour
{
    Transform _transform;
    GameObject CurrentCell;
    
    public bool Placed = false;
    
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
                if(CurrentCell == null)
                {
                    CurrentCell = hit.transform.gameObject;
                }
                if (CurrentCell != hit.transform.gameObject)
                {
                    CurrentCell.GetComponent<Highlighter>().Unhighlight();
                }
                CurrentCell = hit.transform.gameObject;
                CurrentCell.GetComponent<Highlighter>().Highlight();
            }
            
        }
        else if (CurrentCell != null)
        {
            CurrentCell.GetComponent<Highlighter>().Unhighlight();
            CurrentCell = null;
        }
    }

    public void FixToCell()
    {
        if(CurrentCell != null)
        {
            _transform.position = CurrentCell.transform.position;
            CurrentCell.transform.GetComponent<Highlighter>().Occupied = true;
            Placed = true;
        }
    }

    public bool IsOccupied()
    {
        return CurrentCell.transform.GetComponent<Highlighter>().Occupied;
    }
}

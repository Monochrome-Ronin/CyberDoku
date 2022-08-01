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
            CurrentCell.transform.GetComponent<Highlighter>().Cube = _transform.gameObject;
            Placed = true;
        }
    }

    public bool IsOccupied()
    {
        return CurrentCell.transform.GetComponent<Highlighter>().Occupied;
    }

    public bool HaveCell()
    {
        return CurrentCell != null;
    }

    public void UnHighlightCell()
    {
        CurrentCell.GetComponent<Highlighter>().Unhighlight();
    }

    public void MakeEneble(bool eneble)
    {
        foreach (SpriteRenderer sprite in _transform.GetComponentsInChildren<SpriteRenderer>())
        {
            try
            {
                if (!eneble)
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
                }
                else
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
                }
            }
            catch
            {
                continue;
            }
        }
    }
}

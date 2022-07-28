using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShape : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D _SellCollider;
    [SerializeField]
    SwapnManager _SwapnManager;

    RaycastHit2D CurrentHit;
    bool Clicked = false;
    Vector3 offset = new Vector3();
    Vector3 mousePosition;
    Vector3 startShapePosition;

    Coroutine CurrentCoroutine;
    readonly float LerpSpeed = 20f;
    float t = 0;

    void Start()
    {
        
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
                    //
                    startShapePosition = hit.transform.parent.transform.position;
                    __ChangeScale(hit);
                    offset = hit.transform.parent.transform.position - mousePosition;
                    Clicked = true;
                    CurrentHit = hit;
                    EnebleCollider(false);
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
            if(CurrentHit.collider != null)
            {
                //
                FixCubes(CurrentHit);
                if(!IsPlaced(CurrentHit))
                {
                    __ReturnScale(CurrentHit);
                    EnebleCollider(true);
                    CurrentCoroutine = StartCoroutine(ShapeReturn(startShapePosition, CurrentHit.transform.parent.transform.position));
                }
            }

        }
    }

    void __ChangeScale(RaycastHit2D hit)
    {
        hit.transform.parent.transform.localScale = new Vector3(.61f, .61f, 1);
    }

    void __ReturnScale(RaycastHit2D hit)
    {
        hit.transform.parent.transform.localScale = new Vector3(.4f, .4f, 1);
    }

    void FixCubes(RaycastHit2D hit)
    {
        bool _isOccupied = false;
        bool _haveCells = true;
        foreach (Transform child in hit.transform.parent.GetComponentsInChildren<Transform>())
        {

            try
            {
                _haveCells &= child.gameObject.GetComponent<CellChecker>().HaveCell();
            }
            catch
            {
                continue;
            }
        }

        if (_haveCells)
        {
            foreach (Transform child in hit.transform.parent.GetComponentsInChildren<Transform>())
            {

                try
                {
                    _isOccupied |= child.gameObject.GetComponent<CellChecker>().IsOccupied();
                }
                catch
                {
                    continue;
                }
            }
        }

        if (!_isOccupied && _haveCells)
        {
            foreach (Transform child in hit.transform.parent.GetComponentsInChildren<Transform>())
            {

                try
                {
                    child.gameObject.GetComponent<CellChecker>().FixToCell();
                }
                catch
                {
                    continue;
                }
            }
            //shapeCount--(SwapnManager)
            _SwapnManager.shapeCount -= 1;
        }
        else
        {
            foreach (Transform child in hit.transform.parent.GetComponentsInChildren<Transform>())
            {

                try
                {
                    child.gameObject.GetComponent<CellChecker>().UnHighlightCell();
                }
                catch
                {
                    continue;
                }
            }
        }
        
    }

    void EnebleCollider(bool eneble)
    {
        foreach (Transform child in CurrentHit.transform.parent.GetComponentsInChildren<Transform>())
        {
            try
            {
                child.gameObject.GetComponent<BoxCollider2D>().enabled = eneble;
            }
            catch
            {
                continue;
            }
        }
    }

    bool IsPlaced(RaycastHit2D hit)
    {
        return hit.transform.GetComponent<CellChecker>().Placed;
    }

    IEnumerator ShapeReturn(Vector3 startPos, Vector3 currentPos)
    {
        while (true)
        {
            t += Time.deltaTime * LerpSpeed;
            CurrentHit.transform.parent.transform.position = Vector3.Lerp(currentPos, startPos, t);
            if (t >= 1)
            {
                StopCoroutine(CurrentCoroutine);
                t = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}

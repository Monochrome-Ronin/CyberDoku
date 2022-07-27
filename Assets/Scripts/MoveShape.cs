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
    Vector3 offset = new Vector3();
    Vector3 mousePosition;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] audioClips;

    void Start()
    {
        SellSize = _SellCollider.size.x;
        _audioSource = gameObject.GetComponent<AudioSource>();
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
                    __ChangeScale(hit);
                    offset = hit.transform.parent.transform.position - mousePosition;
                    Clicked = true;
                    CurrentHit = hit;
                    EnebleCollider(false);
                  //  _audioSource.PlayOneShot(audioClips[1]);
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
           // _audioSource.PlayOneShot(audioClips[0]);
            Clicked = false;
            if(CurrentHit.collider != null)
            {
                FixCubes(CurrentHit);
                if(!IsPlaced(CurrentHit))
                    EnebleCollider(true);
            }

        }
    }

    void __ChangeScale(RaycastHit2D hit)
    {
        hit.transform.parent.transform.localScale = new Vector3(.61f, .61f, 1);
    }

    void FixCubes(RaycastHit2D hit)
    {
        bool _isOccupied = false;
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

        foreach (Transform child in hit.transform.parent.GetComponentsInChildren<Transform>())
        {

            try
            {
                if(!_isOccupied)
                    child.gameObject.GetComponent<CellChecker>().FixToCell();
            }
            catch
            {
                continue;
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
}

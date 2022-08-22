using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;
    [SerializeField] private GameObject[] spawnPositions;


    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _bestScore;

    GameObject[] SpawnedShapes = new GameObject[3];
    [Header("Shape Count")]
    public int shapeCount;
    void Start()
    {
        SpawnShape();
    }

    void Update()
    {
        GameOver();
        if (shapeCount < 1) SpawnShape();
    }
    private void SpawnShape() 
    {
        for(int spawnIndex = 0; spawnIndex < 3; spawnIndex++)
        {
            var obj = shapes[Random.Range(0, shapes.Length)];
            SpawnedShapes[spawnIndex] = Instantiate(obj, spawnPositions[spawnIndex].transform.position + obj.transform.GetChild(0).position, Quaternion.identity);
            SpawnedShapes[spawnIndex].transform.Rotate(0, 0, 90* Random.Range(1, 4), Space.Self);
            //Debug.Log(SpawnedShapes[spawnIndex].transform.rotation.eulerAngles.z);
        }

        shapeCount = 3;
    }

    void GameOver()
    {
        bool gameOver = true;
        foreach (GameObject shape in SpawnedShapes)
        {
            if (shape != null)
            {
                if(shape.transform.childCount > 1)
                {
                    if (!shape.transform.GetChild(1).GetComponent<CellChecker>().Placed)
                    {
                        bool isPlace = BoardManager.IsSpaceToShape(shape);
                        gameOver &= !isPlace;
                        if (!isPlace) 
                        {
                            EnebleShape(shape, false);
                            
                        }
                        else
                        {
                            EnebleShape(shape, true);
                        }
                    }
                }
            }
        }
        if (gameOver && shapeCount > 0 && BoardManager.CheckAction())
        {
            _currentScore.text = PlayerPrefs.GetInt("Score").ToString();
            _bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
            _gameOverPanel.SetActive(true);
        }
    }

    void EnebleShape(GameObject shape, bool eneble)
    {
        foreach (Transform child in shape.GetComponentsInChildren<Transform>())
        {
            try
            {
                child.gameObject.GetComponent<CellChecker>().Eneble = eneble;
                //child.gameObject.GetComponent<CellChecker>().MakeEneble(eneble);
            }
            catch
            {
                continue;
            }
        }
    }

}



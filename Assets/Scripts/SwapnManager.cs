using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;
    [SerializeField] private GameObject[] spawnPositions;
    private GameObject shapeFinder;
    private int shapeCount;
    void Start()
    {
        SpawnShape();
        shapeCount = 3;
    }

    void Update()
    {
        if (shapeCount < 1) SpawnShape();
    }
    //Для Респавна трёх фигур в метод уничтожения фигуры вставить shapeCount--
    private void SpawnShape() 
    {
        for(int spawnIndex = 0; spawnIndex < 3; spawnIndex++)
        {
            Instantiate(shapes[Random.Range(0, shapes.Length)], spawnPositions[spawnIndex].transform.position, Quaternion.Euler(0, 0, 90 * Random.Range(1, 3)));
        }

        shapeCount = 3;
    }

}

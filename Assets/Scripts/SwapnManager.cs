using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shapes;
    [SerializeField] private GameObject[] spawnPositions;
    public int shapeCount;
    void Start()
    {
        SpawnShape();
    }

    void Update()
    {
        if (shapeCount < 1) SpawnShape();
    }
    //��� �������� ��� ����� � ����� ����������� ������ �������� shapeCount--
    private void SpawnShape() 
    {
        for(int spawnIndex = 0; spawnIndex < 3; spawnIndex++)
        {
            var obj = shapes[Random.Range(0, shapes.Length)];
            Instantiate(obj, spawnPositions[spawnIndex].transform.position + obj.transform.GetChild(0).position, Quaternion.Euler(0, 0, 90 * Random.Range(1, 3)));
        }

        shapeCount = 3;
    }

}

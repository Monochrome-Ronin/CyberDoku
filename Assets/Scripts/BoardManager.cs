using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _Panel;

    [SerializeField]
    Transform[,] MatrixGrid = new Transform[9,9];

    [Header("Animation")]
    Quaternion _rotateTo;
    [SerializeField] private Animator _Cube;
    [SerializeField] private GameObject _laserParticle;
    private GameObject _laserGameObject;
    private bool fawfa = false;

    void Start()
    {
        Transform[] GridElements = _Panel.transform.GetComponentsInChildren<Transform>();
        int count = 1;
        for(int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                MatrixGrid[y, x] = GridElements[count];
                count++;
            }
        }
    }

    void Update()
    {
        RotateRow(CheckRows());
        RotateColumns(CheckColumns());
    }

    int[] CheckRows()
    {
        bool[] OccupiedRows = new bool[9];
        for (int y = 0; y < 9; y++)
        {
            bool Occupied = true;
            for (int x = 0; x < 9; x++)
            {
                Occupied &= MatrixGrid[y, x].GetComponent<Highlighter>().Occupied;
            }
            OccupiedRows[y] = Occupied;
        }
        int count = 0;
        for(int i = 0; i < 9; i++)
        {
            if (OccupiedRows[i])
                count++;
        }
        int[] result = new int[count];
        for (int i = 0; i < 9; i++)
        {
            if (OccupiedRows[i])
            {
                result[count - 1] = i;
                count--;
            }
        }
        return result;
    }

    int[] CheckColumns()
    {
        bool[] OccupiedRows = new bool[9];
        for (int y = 0; y < 9; y++)
        {
            bool Occupied = true;
            for (int x = 0; x < 9; x++)
            {
                Occupied &= MatrixGrid[x, y].GetComponent<Highlighter>().Occupied;
            }
            OccupiedRows[y] = Occupied;
        }
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            if (OccupiedRows[i])
                count++;
        }
        int[] result = new int[count];
        for (int i = 0; i < 9; i++)
        {
            if (OccupiedRows[i])
            {
                result[count - 1] = i;
                count--;
            }
        }
        return result;
    }

    void DeleteRows(int[] rows)
    {
        for(int i = 0; i < rows.Length; i++)
        {
            for(int x = 0; x < 9; x++)
            {
                Destroy(MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube);
                MatrixGrid[rows[i], x].GetComponent<Highlighter>().Occupied = false;
                MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube = null;
                MatrixGrid[rows[i], x].GetComponent<Highlighter>().Unhighlight();
                fawfa = false;
            }
        }
        Destroy(_laserGameObject);
    }
    void RotateRow(int[] rows)
    {
        _rotateTo = Quaternion.Euler(0,0,90);
        for(int i = 0; i < rows.Length; i++)
        {
            for(int x = 0; x < 9; x++)
            {
                MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube.transform.rotation = Quaternion.Slerp(MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube.transform.rotation, _rotateTo, Time.deltaTime * 10f);
            }
            if(!fawfa){
               LaserHorizontal(MatrixGrid[rows[i],0].transform.position.y); 
            }
            Invoke("DestroyingRow", 2f);
        }
    }

    void LaserHorizontal(float y){
            _laserGameObject = Instantiate(_laserParticle, new Vector3(-6, y, 1 ), Quaternion.Euler(0,0,0));
            fawfa = true;
    }
    void DestroyingRow(){
        DeleteRows(CheckRows());
    }

    void DeleteColumns(int[] columns)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            for (int y = 0; y < 9; y++)
            {
                Destroy(MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Cube);
                MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Occupied = false;
                MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Cube = null;
                MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Unhighlight();
                fawfa = false;
            }
        }
        Destroy(_laserGameObject);
    }
    void RotateColumns(int[] columns)
    {
        _rotateTo = Quaternion.Euler(0,0,0);
        for (int i = 0; i < columns.Length; i++)
        {
            for (int y = 0; y < 9; y++)
            {
                MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Cube.transform.rotation = Quaternion.Slerp(MatrixGrid[y, columns[i]].GetComponent<Highlighter>().Cube.transform.rotation, _rotateTo, Time.deltaTime * 3f);
            }
            if(!fawfa){
               LaserVertical(MatrixGrid[0,columns[i]].transform.position.x); 
            }
            Invoke("DestroyingColumn", 2f);
        }
    }

    void LaserVertical(float x){
        
            _laserGameObject = Instantiate(_laserParticle, new Vector3(x, 5.5f, 1 ), Quaternion.Euler(0,0,-90));
            fawfa = true;
    }

    void DestroyingColumn(){
        DeleteColumns(CheckColumns());
    }


}

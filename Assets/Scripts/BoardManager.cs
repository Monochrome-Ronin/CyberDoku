using UnityEngine;
using System.Collections;
using System;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _Panel;
    [SerializeField]
    GameObject shape;

    [SerializeField]
    static Transform[,] MatrixGrid = new Transform[9,9];

    [SerializeField]
    Transform[,,,] MatrixSquareGrid = new Transform[3, 3, 3, 3];

    [Header("Animation")]
    Quaternion _rotateTo;
    [SerializeField] private Animator _Cube;
    [SerializeField] private GameObject _laserParticle;

    [Header("Score")]
    [SerializeField] ScoreManager _scoreManager;

    bool[] LazerPlayedRows = new bool[9];
    GameObject[] _laserGameObjectRows = new GameObject[9];
    bool[] LazerPlayedColumns = new bool[9];
    GameObject[] _laserGameObjectColumns = new GameObject[9];

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
        count = 1;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        MatrixSquareGrid[j, y, x, i] = GridElements[count];
                        count++;
                    }
                }
            }
        }
    }

    void Update()
    {
        RotateRow(CheckRows());
        RotateColumns(CheckColumns());
        RotateSquare(CheckSquare());
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

    Vector2[] CheckSquare()
    {
        bool[,] OccupiedSquare = new bool[3, 3];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                bool Occupied = true;
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Occupied &= MatrixSquareGrid[y, x, j, i].GetComponent<Highlighter>().Occupied;
                    }
                }
                OccupiedSquare[y, x] = Occupied;
            }
        }
        int count = 0;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (OccupiedSquare[y, x])
                    count++;
            }
        }
        
        Vector2[] result = new Vector2[count];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (OccupiedSquare[y, x])
                {
                    result[count - 1] = new Vector2(y, x);
                    count--;
                }
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
            }
            LazerPlayedRows[rows[i]] = false;
            Destroy(_laserGameObjectRows[rows[i]]);
        }
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
            if(!LazerPlayedRows[rows[i]])
            {
                _laserGameObjectRows[rows[i]] = LaserHorizontal(MatrixGrid[rows[i], 0].transform.position.y);
                LazerPlayedRows[rows[i]] = true;
            }
            Invoke("DestroyingRow", 2f);          
        }
    }

    GameObject LaserHorizontal(float y){
        _scoreManager.AddNinePoint();
        return Instantiate(_laserParticle, new Vector3(-6, y, 1), Quaternion.Euler(0, 0, 0));
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
            }
            LazerPlayedColumns[columns[i]] = false;
            Destroy(_laserGameObjectColumns[columns[i]]);
        }
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
            if (!LazerPlayedColumns[columns[i]])
            {
                _laserGameObjectColumns[columns[i]] = LaserVertical(MatrixGrid[0, columns[i]].transform.position.x);
                LazerPlayedColumns[columns[i]] = true;
            }
            Invoke("DestroyingColumn", 2f);
        }
    }

    GameObject LaserVertical(float x)
    {
        _scoreManager.AddNinePoint();
        return Instantiate(_laserParticle, new Vector3(x, 5.5f, 1), Quaternion.Euler(0, 0, -90));
    }

    void DestroyingColumn(){
        DeleteColumns(CheckColumns());
    }

    void DeleteSquares(Vector2[] squares)
    {
        for (int i = 0; i < squares.Length; i++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Destroy(MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Cube);
                    MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Occupied = false;
                    MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Cube = null;
                    MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Unhighlight();
                }
            }
            _scoreManager.AddNinePoint();
        }
    }
    void RotateSquare(Vector2[] squares)
    {
        _rotateTo = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < squares.Length; i++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Cube.transform.rotation = Quaternion.Slerp(MatrixSquareGrid[(int)squares[i].x, (int)squares[i].y, y, x].GetComponent<Highlighter>().Cube.transform.rotation, _rotateTo, Time.deltaTime * 3f);
                    Invoke("DesroyingSquare", 1f);
                }
            }
        }
    }

    void DesroyingSquare()
    {
        DeleteSquares(CheckSquare());
    }

    public static bool IsSpaceToShape(GameObject shape)
    {
        Vector2[] cubes = new Vector2[shape.transform.childCount - 1];
        //get position of cubes due to local position
        for (int i = 1; i < shape.transform.childCount; i++)
        {
            cubes[i - 1] = shape.transform.TransformPoint(shape.transform.GetChild(i).transform.localPosition);
            
        }
        //scele cubes to 1
        for (int i = 1; i < shape.transform.childCount; i++)
        {
            cubes[i - 1] *= new Vector2(2.5f, 2.5f);
        }
        //convert positions to normal vector
        Vector2 offset = cubes[0];
        for (int i = 1; i < shape.transform.childCount; i++)
        {
            cubes[i - 1] -= offset;
        }
        //matrix search for free space
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                bool isSpace = true;
                for (int i = 0; i < cubes.Length; i++)
                {
                    try
                    {
                        isSpace &= !MatrixGrid[y - (int)cubes[i].y, x + (int)cubes[i].x].GetComponent<Highlighter>().Occupied;
                    }
                    catch
                    {
                        isSpace = false;
                    }
                }
                if (isSpace)
                {
                    return true;
                }
            }
        }
        return false;
    }

}

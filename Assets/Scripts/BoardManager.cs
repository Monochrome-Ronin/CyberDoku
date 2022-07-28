using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _Panel;

    [SerializeField]
    Transform[,] MatrixGrid = new Transform[9,9];

    [SerializeField]
    Transform[,,,] MatrixSquareGrid = new Transform[3, 3, 3, 3];

    Quaternion _rotateTo;
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
        }
    }
    void RotateRow(int[] rows)
    {
        _rotateTo = Quaternion.Euler(0,0,0);
        for(int i = 0; i < rows.Length; i++)
        {
            for(int x = 0; x < 9; x++)
            {
                MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube.transform.rotation = Quaternion.Slerp(MatrixGrid[rows[i], x].GetComponent<Highlighter>().Cube.transform.rotation, _rotateTo, Time.deltaTime * 3f);
                Invoke("DesroyingRow", 1f);
            }
        }
    }
    void DesroyingRow(){
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
                Invoke("DesroyingColumn", 1f);
            }
        }
    }
    void DesroyingColumn(){
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
}

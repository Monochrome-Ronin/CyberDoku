using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    GameObject _Panel;

    [SerializeField]
    Transform[,] MatrixGrid = new Transform[9,9];

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
        DeleteRows(CheckRows());
        DeleteColumns(CheckColumns());
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
            }
        }
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
}

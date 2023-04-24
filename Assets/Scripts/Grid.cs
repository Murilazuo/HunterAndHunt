using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject gridCellPrefab;
    [SerializeField] Vector2Int gridSize;
    Agent[,] cells;

    public static Grid Instance;
    private void Awake()
    {
        Instance = this;

        cells = new Agent[gridSize.x, gridSize.y];
        for(int x = 0; x < gridSize.x; x++)
            for(int y = 0; y < gridSize.y; y++)
                Instantiate(gridCellPrefab, new Vector2(x, y),Quaternion.identity).transform.SetParent(transform);
    }
    public Agent GetCell(Vector2Int pos)
    {
        return GetCell(pos.x, pos.y) ;
    }
    public Agent GetCell(int x, int y)
    {

        if (InBounds(x, y))
            return cells[x, y];
        else
            return null;
    }
    public void SetCell(Vector2Int pos, Agent value)
    {
        cells[pos.x, pos.y] = value;
    }
    public void DestroyCell(Vector2Int pos)
    {
        GetCell(pos).DestroyAgent();   
        SetCell(pos, null);
    }
    public bool InBounds(Vector2Int pos)
    {
        return InBounds(pos.x, pos.y);
    }
    public bool InBounds(int x,int y)
    {
        return x < gridSize.x && y < gridSize.y && x >= 0 && y >= 0;
    }
    public int MaxCells()
    {
        return gridSize.x * gridSize.y;
    }
    public Vector2Int RandomPosInBounds()
    {
        Vector2Int result = new();
        result.x = Random.Range(0, gridSize.x);
        result.y = Random.Range(0, gridSize.y);
        return result;
    }

    private void OnDrawGizmos()
    {
        Vector3 center = new(gridSize.x - 1, gridSize.y - 1, 0);
        center /= 2;
        Vector3 size = new(gridSize.x ,gridSize.y, 0);
        
        Gizmos.DrawWireCube(center, Vector3.one);
        Gizmos.DrawWireCube(center, size);
    }
}

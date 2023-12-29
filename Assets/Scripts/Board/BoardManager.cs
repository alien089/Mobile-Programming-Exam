using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int m_GridWidth;
    [SerializeField] private int m_GridHeight;
    private Grid<Tile> m_Grid;

    public Grid<Tile> Grid { get => m_Grid; set => m_Grid = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 pos0 = new Vector3();
        Vector3 pos1 = new Vector3();
        for (int i = 0; i < m_GridWidth + 1; i++)
        {
            pos0.x = i;
            pos0.z = 0;
            pos1.x = i;
            pos1.z = m_GridHeight;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pos0, pos1);
        }

        for (int i = 0; i < m_GridHeight + 1; i++)
        {
            pos0.x = 0;
            pos0.z = i;
            pos1.x = m_GridWidth;
            pos1.z = i;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pos0, pos1);
        }

        if (!Application.isPlaying)
        {
            m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile());
            for (int i = 0; i < m_GridWidth; i++)
            {
                for (int j = 0; j < m_GridHeight; j++)
                {
                    List<Ingreditent> tmp = new List<Ingreditent>();

                    if (GetComponent<BoardData>().BreadPositions.Contains(new Vector2(i, j)))
                        tmp.Add(new Ingreditent(IngreditType.Bread));

                    else if (GetComponent<BoardData>().CheesePositions.Contains(new Vector2(i, j)))
                        tmp.Add(new Ingreditent(IngreditType.Cheese));

                    else if (GetComponent<BoardData>().TomatoPositions.Contains(new Vector2(i, j)))
                        tmp.Add(new Ingreditent(IngreditType.Tomato));

                    else if (GetComponent<BoardData>().SaladPositions.Contains(new Vector2(i, j)))
                        tmp.Add(new Ingreditent(IngreditType.Salad));

                    m_Grid.GetGridObject(i, j).AddToStack(tmp);
                }
            }
        }

        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                for(int x = 0; x < m_Grid.GetGridObject(i, j).IngredientsStack.Count; x++)
                {
                    Vector3 pos = m_Grid.GetWorldPosition(i, j);
                    pos = new Vector3(pos.x, x, pos.z);

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Cheese)
                        Gizmos.color = Color.yellow;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Salad)
                        Gizmos.color = Color.green;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Tomato)
                        Gizmos.color = Color.red;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Bread)
                        Gizmos.color = Color.black;


                    Gizmos.DrawCube(pos, new Vector3(m_Grid.GetCellSize(), 0.2f, m_Grid.GetCellSize()));
                }
            }
        }
    }

    private void Awake()
    {
        m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile());
        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                List<Ingreditent> tmp = new List<Ingreditent>();

                if (GetComponent<BoardData>().BreadPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Bread));

                else if (GetComponent<BoardData>().CheesePositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Cheese));

                else if (GetComponent<BoardData>().TomatoPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Tomato));

                else if (GetComponent<BoardData>().SaladPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Salad));

                m_Grid.GetGridObject(i, j).AddToStack(tmp);
            }
        }

        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                for (int x = 0; x < m_Grid.GetGridObject(i, j).IngredientsStack.Count; x++)
                {
                    Vector3 pos = m_Grid.GetWorldPosition(i, j);
                    pos = new Vector3(pos.x, x, pos.z);
                    Color tmp = Color.cyan;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Cheese)
                        tmp = Color.yellow;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Salad)
                        tmp = Color.green;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Tomato)
                        tmp = Color.red;

                    if (m_Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Bread)
                        tmp = Color.black;

                    GameObject go = Instantiate(GetComponent<BoardData>().Tile, pos, Quaternion.identity, transform);
                    go.GetComponent<MeshRenderer>().material.color = tmp;
                }
            }
        }
    }

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, ChangeTile);
    }

    public void ChangeTile(object[] param)
    {
        Vector3 startPos = (Vector3)param[0];
        Vector3 endPos = (Vector3)(param[1]);
         
        if (endPos == Vector3.positiveInfinity) 
            return;

        Vector2 startCell;
        Vector2 endCell;

        m_Grid.GetXY(startPos, out int x, out int y);
        startCell = new Vector2(x, y);
        
        m_Grid.GetXY(endPos, out int a, out int b);
        endCell = new Vector2(a, b);

        if (endCell.x != startCell.x && endCell.y != startCell.y) return;
        if (endCell.x == startCell.x && endCell.y == startCell.y) return;
        if (Mathf.Abs(endCell.x - startCell.x) > 1 || Mathf.Abs(endCell.y - startCell.y) > 1) return;
        if (m_Grid.GetGridObject(startCell).IngredientsStack.Count == 0 || m_Grid.GetGridObject(endCell).IngredientsStack.Count == 0) return;

        List<Ingreditent> ingredients = m_Grid.GetRefGridObject(startPos).IngredientsStack;
        List<Ingreditent> ingredient = m_Grid.GetRefGridObject(endPos).IngredientsStack;
        
        m_Grid.GetRefGridObject(endPos).AddToStack(m_Grid.GetRefGridObject(startPos).IngredientsStack);
        m_Grid.GetRefGridObject(startPos).IngredientsStack.Clear();
    }
}
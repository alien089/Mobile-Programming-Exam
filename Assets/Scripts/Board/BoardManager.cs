using System.Collections;
using System.Collections.Generic;
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

        if (Application.isEditor)
        {
            m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile());
            for (int i = 0; i < m_GridWidth; i++)
            {
                for (int j = 0; j < m_GridHeight; j++)
                {
                    if (GetComponent<BoardData>().BreadPositions.Contains(new Vector2(i, j)))
                        m_Grid.GetGridObject(i, j).AddToStack(new Ingreditent(IngreditType.Bread));

                    else if (GetComponent<BoardData>().CheesePositions.Contains(new Vector2(i, j)))
                        m_Grid.GetGridObject(i, j).AddToStack(new Ingreditent(IngreditType.Cheese));

                    else if (GetComponent<BoardData>().TomatoPositions.Contains(new Vector2(i, j)))
                        m_Grid.GetGridObject(i, j).AddToStack(new Ingreditent(IngreditType.Tomato));

                    else if (GetComponent<BoardData>().SaladPositions.Contains(new Vector2(i, j)))
                        m_Grid.GetGridObject(i, j).AddToStack(new Ingreditent(IngreditType.Salad));
                }
            }
        }

        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                //Debug.Log("Node type: " + AssetData.Grid.GetGridObject(i, j).NodeType.ToString());
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
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, ChangeTile);
        m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile());
    }

    public void ChangeTile(object[] param)
    {

    }
}

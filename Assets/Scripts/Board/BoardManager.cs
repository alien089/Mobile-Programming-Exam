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
    private BoardData m_BoardData; 
    public bool Undo = false;

    public Grid<Tile> Grid { get => m_Grid; }
    public int GridWidth { get => m_GridWidth; }
    public int GridHeight { get => m_GridHeight; }
    public BoardData BoardData { get => m_BoardData; }

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
            m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile(x, y));
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
                    pos = new Vector3(pos.x, x*0.3f, pos.z);

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
        m_BoardData = GetComponent<BoardData>();
        m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile(x, y));
        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                List<Ingreditent> tmp = new List<Ingreditent>();

                if (m_BoardData.BreadPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Bread));

                else if (m_BoardData.CheesePositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Cheese));

                else if (m_BoardData.TomatoPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Tomato));

                else if (m_BoardData.SaladPositions.Contains(new Vector2(i, j)))
                    tmp.Add(new Ingreditent(IngreditType.Salad));

                m_Grid.GetGridObject(i, j).AddToStack(tmp);
            }
        }
    }

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.MOVEMENT_PLAYER, ChangeTile);
        GameManager.instance.EventManager.TriggerEvent(Constants.GENERATE_TILES, m_GridWidth, m_GridHeight, this);

    }

    public void ChangeTile(object[] param)
    {
        if(!Undo)
        {
            Vector3 startPos = (Vector3)param[0];
            Vector3 endPos = (Vector3)(param[1]);

            if (endPos == Vector3.positiveInfinity)
                return;

            CalcEndPos(startPos, endPos, out int x, out int y);

            if (m_Grid.GetGridObject(startPos) == null || m_Grid.GetGridObject(x, y) == null) return;
            if (m_Grid.GetGridObject(startPos).IngredientsStack == null || m_Grid.GetGridObject(x, y).IngredientsStack == null) return;
            if (m_Grid.GetGridObject(startPos).IngredientsStack.Count == 0 || m_Grid.GetGridObject(x, y).IngredientsStack.Count == 0) return;

            Vector3 calculatedEndPos = new Vector3(x, 0, y);

            if (m_Grid.GetGridObject(startPos).X == m_Grid.GetGridObject(calculatedEndPos).X && m_Grid.GetGridObject(startPos).Y == m_Grid.GetGridObject(calculatedEndPos).Y) return;

            List<Ingreditent> ingredients = m_Grid.GetRefGridObject(startPos).IngredientsStack;
            List<Ingreditent> ingredient = m_Grid.GetRefGridObject(calculatedEndPos).IngredientsStack;

            Tile movedFrom = CreateNewTile(startPos);
            Tile movedTo = CreateNewTile(calculatedEndPos);

            GameManager.instance.EventManager.TriggerEvent(Constants.REGISTER_UNDO, movedFrom, movedTo);

            m_Grid.GetRefGridObject(calculatedEndPos).AddToStack(m_Grid.GetRefGridObject(startPos).FlipStack());
            m_Grid.GetRefGridObject(startPos).IngredientsStack.Clear();

            GameManager.instance.EventManager.TriggerEvent(Constants.GENERATE_TILES, m_GridWidth, m_GridHeight, this);
            GameManager.instance.EventManager.TriggerEvent(Constants.WIN_GAME, this, calculatedEndPos); 
        }
        else
        {
            Undo = false;
        }
    }

    private void CalcEndPos(Vector3 startPos, Vector3 endPos, out int x, out int y)
    {
        Vector2 dir = Vector2.zero;

        float X = endPos.x - startPos.x;
        float Y = endPos.z - startPos.z;

        if (Mathf.Abs(X) <= m_BoardData.DpadSenseX && Mathf.Abs(Y) <= m_BoardData.DpadSenseY)
            dir = Vector2.zero;
        else if (Mathf.Abs(X) > Mathf.Abs(Y))
            dir = X > 0 ? new Vector2(1f, 0f) : new Vector2(-1f, 0f); //right || left
        else
            dir = Y > 0 ? new Vector2(0f, 1f) : new Vector2(0f, -1f); //up !! down

        m_Grid.GetXY(startPos, out int a, out int b);

        x = a + (int)dir.x;
        y = b + (int)dir.y;
    }

    private Tile CreateNewTile(Vector3 pos)
    {
        m_Grid.GetXY(pos, out int a, out int b);
        Tile rtn = new Tile(a, b);

        rtn.AddToStack(m_Grid.GetGridObject(pos).IngredientsStack);
        
        return rtn;
    }
}
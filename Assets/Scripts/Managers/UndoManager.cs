using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private Move m_MoveFrom;
    private Move m_MoveTo;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventManager.Register(Constants.REGISTER_UNDO, MoveDone);
        GameManager.instance.EventManager.Register(Constants.DO_UNDO, UndoMove);
    }

    public void MoveDone(object[] param)
    {
        m_MoveFrom.Tile = (Tile)param[0];
        m_MoveTo.Tile = (Tile)param[1];
    }

    public void UndoMove(object[] param)
    {
        BoardManager boardManager = (BoardManager)param[0];
        boardManager.Grid.SetGridObject(m_MoveFrom.Tile.X, m_MoveFrom.Tile.Y, m_MoveFrom.Tile);
        boardManager.Grid.SetGridObject(m_MoveTo.Tile.X, m_MoveTo.Tile.Y, m_MoveTo.Tile);
    }
}

public struct Move
{
    public Tile Tile;
    public Vector3 Position;
}
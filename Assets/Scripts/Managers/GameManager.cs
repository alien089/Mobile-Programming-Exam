using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.SingletonPattern;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private EventManager m_EventManager = Factory.CreateEventManager();
    public EventManager EventManager { get => m_EventManager; }

    private void Start()
    {
        instance.EventManager.Register(Constants.WIN_GAME, WinGame);
    }

    public void WinGame(object[] param)
    {
        BoardManager boardManager = (BoardManager)param[0];
        Vector3 pos = (Vector3)param[1];

        int countTotalTiles = 0;
        countTotalTiles += boardManager.BoardData.BreadPositions.Count;
        countTotalTiles += boardManager.BoardData.CheesePositions.Count;
        countTotalTiles += boardManager.BoardData.SaladPositions.Count;
        countTotalTiles += boardManager.BoardData.TomatoPositions.Count;

        if(boardManager.Grid.GetGridObject(pos).IngredientsStack.Count == countTotalTiles)
        {
            if (boardManager.Grid.GetGridObject(pos).IngredientsStack[0].Type == IngreditType.Bread && 
                boardManager.Grid.GetGridObject(pos).IngredientsStack[countTotalTiles - 1].Type == IngreditType.Bread)
                instance.EventManager.TriggerEvent(Constants.SHOW_WIN);
        }        
    }
}

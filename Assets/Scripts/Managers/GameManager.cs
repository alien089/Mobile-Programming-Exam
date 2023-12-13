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
        Debug.Log("Win");
    }
}

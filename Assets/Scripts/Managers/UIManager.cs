using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BoardManager m_BoardManager; 

    public void Undo()
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.DO_UNDO, m_BoardManager);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

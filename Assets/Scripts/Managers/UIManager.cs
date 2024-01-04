using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BoardManager m_BoardManager; 
    [SerializeField] private TextMeshProUGUI m_WinLabel;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.SHOW_WIN, ShowWinLabel);
    }

    public void Undo()
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.DO_UNDO, m_BoardManager);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinLabel(object[] param)
    {
        m_WinLabel.gameObject.SetActive(true);
    }
}
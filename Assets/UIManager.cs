using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BoardManager m_BoardManager; 

    public void Undo()
    {
        m_BoardManager.m_Grid = m_BoardManager.m_PreviusMoveGrid;
    }
}

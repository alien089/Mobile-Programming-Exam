using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : MonoBehaviour
{
    [SerializeField] private List<Vector2> m_BreadPositions;
    [SerializeField] private List<Vector2> m_SaladPositions;
    [SerializeField] private List<Vector2> m_CheesePositions;
    [SerializeField] private List<Vector2> m_TomatoPositions;
    [SerializeField] private GameObject m_Tile; 
    [SerializeField] private float m_DpadSenseX; 
    [SerializeField] private float m_DpadSenseY; 

    public List<Vector2> BreadPositions { get => m_BreadPositions;}
    public List<Vector2> SaladPositions { get => m_SaladPositions;}
    public List<Vector2> CheesePositions { get => m_CheesePositions;}
    public List<Vector2> TomatoPositions { get => m_TomatoPositions;}
    public GameObject Tile { get => m_Tile; set => m_Tile = value; }
    public float DpadSenseX { get => m_DpadSenseX; set => m_DpadSenseX = value; }
    public float DpadSenseY { get => m_DpadSenseY; set => m_DpadSenseY = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : MonoBehaviour
{
    [SerializeField] private List<Vector2> m_BreadPositions;
    [SerializeField] private List<Vector2> m_SaladPositions;
    [SerializeField] private List<Vector2> m_CheesePositions;
    [SerializeField] private List<Vector2> m_TomatoPositions;

    public List<Vector2> BreadPositions { get => m_BreadPositions;}
    public List<Vector2> SaladPositions { get => m_SaladPositions;}
    public List<Vector2> CheesePositions { get => m_CheesePositions;}
    public List<Vector2> TomatoPositions { get => m_TomatoPositions;}
}

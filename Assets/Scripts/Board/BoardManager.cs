using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Gizmos.DrawLine(pos0, pos1);
        }

        for (int i = 0; i < m_GridHeight + 1; i++)
        {
            pos0.x = 0;
            pos0.z = i;
            pos1.x = m_GridWidth;
            pos1.z = i;
            Gizmos.DrawLine(pos0, pos1);
        }
    }

    private void Awake()
    {
        m_Grid = new Grid<Tile>(m_GridWidth, m_GridHeight, 1, transform.position, (int x, int y) => new Tile());
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

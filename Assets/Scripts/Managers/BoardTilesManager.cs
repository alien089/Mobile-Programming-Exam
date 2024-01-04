using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTilesManager : MonoBehaviour
{
    [SerializeField] private BoardData m_BoardData;
    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.GENERATE_TILES, GenerateTiles);
    }

    public void GenerateTiles(object[] param)
    {
        int m_GridWidth = (int)param[0];
        int m_GridHeight = (int)param[1];
        BoardManager boardManager = (BoardManager)param[2];

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                for (int x = 0; x < boardManager.Grid.GetGridObject(i, j).IngredientsStack.Count; x++)
                {
                    Vector3 pos = boardManager.Grid.GetWorldPosition(i, j);
                    pos = new Vector3(pos.x, x*0.3f, pos.z);

                    Color tmp = Color.cyan;

                    if (boardManager.Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Cheese)
                        tmp = Color.yellow;

                    if (boardManager.Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Salad)
                        tmp = Color.green;

                    if (boardManager.Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Tomato)
                        tmp = Color.red;

                    if (boardManager.Grid.GetGridObject(i, j).IngredientsStack[x].Type == IngreditType.Bread)
                        tmp = Color.black;

                    GameObject go = Instantiate(m_BoardData.Tile, pos, Quaternion.identity, transform);
                    go.GetComponent<MeshRenderer>().material.color = tmp;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private List<Ingreditent> m_IngredientsStack = new List<Ingreditent>();

    public List<Ingreditent> IngredientsStack { get => m_IngredientsStack;}

    public void AddToStack(List<Ingreditent> ingreditent)
    {
        if (m_IngredientsStack == null)
            m_IngredientsStack = new List<Ingreditent>();

        foreach(Ingreditent ing in ingreditent)
            m_IngredientsStack.Add(ing);
    }

    public void ClearStack()
    {
        m_IngredientsStack.Clear();
    }
}

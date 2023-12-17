using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private List<Ingreditent> m_IngredientsStack = new List<Ingreditent>();

    public List<Ingreditent> IngredientsStack { get => m_IngredientsStack; set => m_IngredientsStack = value; }

    public void AddToStack(Ingreditent ingreditent)
    {
        if (m_IngredientsStack == null)
            m_IngredientsStack = new List<Ingreditent>();

         m_IngredientsStack.Add(ingreditent);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private List<Ingreditent> m_IngredientsStack;

    public List<Ingreditent> IngredientsStack { get => m_IngredientsStack; set => m_IngredientsStack = value; }
}

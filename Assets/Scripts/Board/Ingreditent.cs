using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingreditent
{
    private IngreditType m_Type;

    public IngreditType Type { get => m_Type;}

    public Ingreditent(IngreditType type)
    {
        m_Type = type;
    }
}

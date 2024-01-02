using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Generics.Pattern.StatePattern;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.Playables;

public class InputStateManager : StatesMachine<InputState>
{
    public Vector3 m_TouchStartPosition;
    public Vector3 m_TouchEndPosition = Vector3.positiveInfinity;
    
    public GameObject m_Grid;

    public InputStateManager(GameObject grid) : base()
    {
        m_Grid = grid;
    }

    protected override void InitStates()
    {
        StatesList.Add(InputState.Idle, new InputdleState(InputState.Idle, this));
        StatesList.Add(InputState.Began, new InputBeganState(InputState.Began, this));
        StatesList.Add(InputState.Moved, new InputMovedState(InputState.Moved, this));
        StatesList.Add(InputState.Ended, new InputEndedState(InputState.Ended, this));
    }

    public Vector3 GetTouchWorldSpace()
    {
        Vector3 touchPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Plane plane = new(Vector3.up, m_Grid.transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            touchPosition = ray.GetPoint(distance); 
        }
        touchPosition.y = m_Grid.transform.position.y;

        return touchPosition;
    }
}

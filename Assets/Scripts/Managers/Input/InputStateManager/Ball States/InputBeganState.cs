using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBeganState : State<InputState>
{
    private InputStateManager m_InputStatesManager;
    public InputBeganState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_InputStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Vector3 touchPosition = m_InputStatesManager.GetTouchWorldSpace();
        m_InputStatesManager.m_TouchStartPosition = touchPosition;
    }
}

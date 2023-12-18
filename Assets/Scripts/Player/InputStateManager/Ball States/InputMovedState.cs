using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovedState : State<InputState>
{
    private InputStateManager m_InputStatesManager;
    public InputMovedState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_InputStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_InputStatesManager.m_TouchEndPosition = m_InputStatesManager.GetTouchWorldSpace();
    }
}
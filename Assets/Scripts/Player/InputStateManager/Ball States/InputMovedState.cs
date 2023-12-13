using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovedState : State<InputState>
{
    private InputStateManager m_BallStatesManager;
    public InputMovedState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_BallStatesManager.m_TouchEndPosition = m_BallStatesManager.GetTouchWorldSpace();
    }
}
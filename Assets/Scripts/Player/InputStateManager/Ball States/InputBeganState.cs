using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBeganState : State<InputState>
{
    private InputStateManager m_BallStatesManager;
    public InputBeganState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Vector3 touchPosition = m_BallStatesManager.GetTouchWorldSpace();
        m_BallStatesManager.m_TouchStartPosition = touchPosition;
    }
}

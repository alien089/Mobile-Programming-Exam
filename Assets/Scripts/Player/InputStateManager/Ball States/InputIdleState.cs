using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputdleState : State<InputState>
{
    private InputStateManager m_BallStatesManager;
    public InputdleState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        return;
    }
}
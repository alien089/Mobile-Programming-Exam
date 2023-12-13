using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEndedState : State<InputState>
{
    private InputStateManager m_BallStatesManager;
    public InputEndedState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_BallStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_BallStatesManager.m_TouchTempPosition, m_BallStatesManager.m_TouchEndPosition);
    }
}
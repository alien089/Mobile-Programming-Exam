using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEndedState : State<InputState>
{
    private InputStateManager m_InputStatesManager;
    public InputEndedState(InputState stateID, StatesMachine<InputState> stateMachine = null) : base(stateID, stateMachine)
    {
        m_InputStatesManager = (InputStateManager)stateMachine;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        GameManager.instance.EventManager.TriggerEvent(Constants.MOVEMENT_PLAYER, m_InputStatesManager.m_TouchStartPosition, m_InputStatesManager.m_TouchEndPosition);
    }
}
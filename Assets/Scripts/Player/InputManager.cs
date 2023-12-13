using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Grid;
    [SerializeField] private Transform m_Camera;
    
    private InputStateManager m_InputStateManager;

    private void Start()
    {
        m_InputStateManager = new InputStateManager(m_Grid);

        m_InputStateManager.CurrentState = m_InputStateManager.StatesList[InputState.Idle];
    }


    // Update is called once per frame
    void Update()
    {
        bool isTouching = SetState();

        m_InputStateManager.CurrentState.OnUpdate();
    }

    private bool SetState()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) { m_InputStateManager.ChangeState(InputState.Began); return true; }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                m_InputStateManager.ChangeState(InputState.Moved);
                return true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                m_InputStateManager.ChangeState(InputState.Ended);
                return true;
            }
            else { return true; }
        }
        else { m_InputStateManager.ChangeState(InputState.Idle); return false; }
    }

    public Touch GetTouch(int index)
    {
        return Input.GetTouch(index);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    //protected IState curState;
    protected IState entryState;
    public IState mainCurState { get; private set; }

    protected virtual void Awake()
    {
        

    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(mainCurState != null)
        {
            SetState(entryState);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(mainCurState != null)
        {
            mainCurState.OnStateUpdate();
        }
    }
    public void SetState(IState state)
    {
        if (mainCurState != null)
        {
            mainCurState.OnStateExit();
        }

        mainCurState = state;
        mainCurState.OnStateEnter();
    }

    protected virtual void OnDrawGizmos()
    {
        if(mainCurState != null)
        {
            mainCurState.DrawStateGizmos();
        }
    }
}

public interface IState
{
    public void OnStateEnter();
    public void OnStateUpdate();
    public void OnStateExit();

    public void DrawStateGizmos();
}

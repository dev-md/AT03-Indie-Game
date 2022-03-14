using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//------------------------------------------------------
public class EnemySCR : FiniteStateMachine
{
    public Bounds bounds;
    //245
    public float viewRadius = 7f;
    public Transform playerTran;
    public NavMeshAgent _Agent { get; private set; }


    // Start is called before the first frame update

    protected override void Awake()
    {
        entryState = new EnemyIdleST(this);

        //mainCurState = entryState;
        if (TryGetComponent(out NavMeshAgent agent) == true)
        {
            _Agent = agent;
        }
    }
    protected override void Start()
    {
        base.Start();
        SetState(entryState);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}

//------------------------------------------------------
//------------------------------------------------------
//------------------------------------------------------

public abstract class EnemyBHST : IState
{
    protected EnemySCR _Instance { get; private set; }

    public EnemyBHST(EnemySCR instance) 
    {
        _Instance = instance;
    }
    
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public virtual void DrawStateGizmos()
    {

    }

}

//------------------------------------------------------
//------------------------------------------------------
//------------------------------------------------------

public class EnemyIdleST : EnemyBHST
{
    private float _time = -1;
    private Vector2 idleTimeRange = new Vector2(1f,3f);
    private float idleTime = 0;


    public EnemyIdleST(EnemySCR instance) : base(instance)
    {
        //
    }
    
    public override void OnStateEnter()
    {
        //Debug.Log("start Idle");
        _Instance._Agent.isStopped = true;
        idleTime = Random.Range(idleTimeRange.x,idleTimeRange.y);
        _time = 0;
        Debug.Log(idleTime);
    }
    public override void OnStateUpdate()
    {
        if (Vector3.Distance(_Instance.transform.position, _Instance.playerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(new EnemyChaseST(_Instance));
        }

        if (_time >= 0)
        {
            _time += Time.deltaTime;
            if (_time >= idleTime)
            {
                //
                _Instance.SetState(new EnemyWanderST(_Instance));

            }
        }
    }

    public override void OnStateExit()
    {
        _time = -1;
        idleTime = 0;
        //Debug.Log("Exiting Idle");
    }
}

//------------------------------------------------------
public class EnemyWanderST : EnemyBHST
{
    private Vector3 targetPOS;
    private float wanderSpeed = 3.5f;
    public EnemyWanderST(EnemySCR instance) : base(instance)
    {

    }

    public override void OnStateEnter()
    {
        _Instance._Agent.speed = wanderSpeed;
        
        Debug.Log("wander start");
        //
        _Instance._Agent.isStopped = false;
        Vector3 randomPosInBounds = new Vector3
            (
            Random.Range(-_Instance.bounds.extents.x, _Instance.bounds.extents.x),
            _Instance.transform.position.y,
            Random.Range(-_Instance.bounds.extents.z, _Instance.bounds.extents.z)
            );
        targetPOS = randomPosInBounds;
        _Instance._Agent.SetDestination(targetPOS);
        //
    }

    public override void OnStateExit()
    {
        Debug.Log("wander End");
    }

    public override void OnStateUpdate()
    {
        //Debug.Log(Vector3.Distance(targetPOS, _Instance.transform.position));
        //Debug.Log(_Instance._Agent.stoppingDistance);
        if (Vector3.Distance(_Instance.transform.position, _Instance.playerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(new EnemyChaseST(_Instance));
        }


        if (Vector3.Distance(targetPOS, _Instance.transform.position) <= _Instance._Agent.stoppingDistance)
        {
            //Debug.Log("AHOY");
            _Instance.SetState(new EnemyIdleST(_Instance));
        }
    }
}

public class EnemyChaseST : EnemyBHST
{
    private float chaseSpeed = 10f;

    public EnemyChaseST(EnemySCR instance) : base(instance)
    {

    }

    public override void OnStateEnter()
    {
        _Instance._Agent.speed = chaseSpeed;
        _Instance._Agent.isStopped = false;
        Debug.Log("AHOY!");
    }

    public override void OnStateExit()
    {
        Debug.Log("boo.");
    }

    public override void OnStateUpdate()
    {
        //if (_Instance.targetPlayer != null)
        //{
        //    _Instance._Agent.SetDestination(_Instance.targetPlayer.position);
        //}
        //else
        //{
        //    _Instance.SetState(new EnemyWanderST(_Instance));
        //}

        if(Vector3.Distance(_Instance.transform.position, _Instance.playerTran.position) > _Instance.viewRadius*2)
        {
            _Instance.SetState(new EnemyIdleST(_Instance));
        }

        _Instance._Agent.SetDestination(_Instance.playerTran.position);
    }
}
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
    [SerializeField] private Transform playerTran;
    public NavMeshAgent _Agent { get; private set; }

    private Outline outlineObject;
    private bool isStunned;
    private bool foundPos = false;


    // Start is called before the first frame update

    protected override void Awake()
    {
        entryState = new EnemyIdleST(this,playerTran);

        //mainCurState = entryState;
        if (TryGetComponent(out NavMeshAgent agent) == true)
        {
            _Agent = agent;
        }

        outlineObject = GetComponent<Outline>();
        outlineObject.enabled = false;
        isStunned = false;
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

        if ((Input.GetButtonDown("Fire1")) && (outlineObject.enabled == true))
        {
            //Debug.Log("Stunned");
            SetState(new EnemyStunST(this,playerTran));
            isStunned = true;
            StartCoroutine(StunTimer());
        }
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(4);
        isStunned = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    private void OnMouseOver()
    {
        //Debug.Log("LOOK AT ME");
        if (isStunned == false)
        {
            if (
                (outlineObject.enabled == false) &&
                (Vector3.Distance(transform.position, playerTran.position) <= viewRadius+2f)
                )

            {
                outlineObject.enabled = true;
            }
        }
    }
    private void OnMouseExit()
    {
        if (
            (outlineObject.enabled == true) || 
            (Vector3.Distance(transform.position, playerTran.position) > viewRadius)
            )
        { 
            outlineObject.enabled = false;
        }
    }

}

//------------------------------------------------------
//------------------------------------------------------
//------------------------------------------------------

public abstract class EnemyBHST : IState
{
    protected EnemySCR _Instance { get; private set; }
    protected Transform _PlayerTran { get; private set; }

    public EnemyBHST(EnemySCR instance, Transform playerTran) 
    {
        _Instance = instance;
        _PlayerTran = playerTran;
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
    private Vector2 idleTimeRange = new Vector2(3f,10f);
    private float idleTime = 0;


    public EnemyIdleST(EnemySCR instance, Transform playerTran) : base(instance, playerTran)
    {
        //
    }
    
    public override void OnStateEnter()
    {
        //Debug.Log("start Idle");
        _Instance._Agent.isStopped = true;
        idleTime = Random.Range(idleTimeRange.x,idleTimeRange.y);
        _time = 0;
        //Debug.Log(idleTime);
    }
    public override void OnStateUpdate()
    {
        if (Vector3.Distance(_Instance.transform.position, _PlayerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(new EnemyChaseST(_Instance, _PlayerTran));
        }

        if (_time >= 0)
        {
            _time += Time.deltaTime;
            if (_time >= idleTime)
            {
                //
                _Instance.SetState(new EnemyWanderST(_Instance, _PlayerTran));

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
    private bool foundPos = false;
    public EnemyWanderST(EnemySCR instance, Transform playerTran) : base(instance, playerTran)
    {

    }

    public override void OnStateEnter()
    {
        _Instance._Agent.speed = wanderSpeed;
        
        Debug.Log("wander start");
        //
        _Instance._Agent.isStopped = false;
    }

    public override void OnStateExit()
    {
        //Debug.Log("wander End");
        foundPos = false;
    }

    public override void OnStateUpdate()
    {
        //
        //Debug.Log(Vector3.Distance(targetPOS, _Instance.transform.position));
        //Debug.Log(_Instance._Agent.stoppingDistance);
        if (foundPos == false)
        {
            //Debug.Log("Check");
            Vector3 randomPosInBounds = new Vector3
            (
            Random.Range(-_Instance.bounds.extents.x, _Instance.bounds.extents.x),
            _Instance.transform.position.y,
            Random.Range(-_Instance.bounds.extents.z, _Instance.bounds.extents.z)
            );
            targetPOS = randomPosInBounds;
            Debug.Log(targetPOS);

            NavMeshHit hitMesh;
            if (NavMesh.SamplePosition(targetPOS, out hitMesh, 20f, NavMesh.AllAreas))
            {
                _Instance._Agent.SetDestination(hitMesh.position);
                foundPos = true;
            }
        }

        if (Vector3.Distance(_Instance.transform.position, _PlayerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(new EnemyChaseST(_Instance, _PlayerTran));
        }
 
        if (Vector3.Distance(new Vector3(targetPOS.x, _Instance.transform.position.y, targetPOS.z), _Instance.transform.position) <= _Instance._Agent.stoppingDistance)
        {
            //Debug.Log("AHOY IDLE");
            _Instance.SetState(new EnemyIdleST(_Instance, _PlayerTran));
        }
    }
}

public class EnemyChaseST : EnemyBHST
{
    private float chaseSpeed = 10f;

    public EnemyChaseST(EnemySCR instance, Transform playerTran) : base(instance, playerTran)
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

        if(Vector3.Distance(_Instance.transform.position, _PlayerTran.position) > _Instance.viewRadius*4.5f)
        {
            _Instance.SetState(new EnemyIdleST(_Instance, _PlayerTran));
        }

        _Instance._Agent.SetDestination(_PlayerTran.position);
    }
}

public class EnemyStunST : EnemyBHST
{
    private float _time = -1;
    private Vector2 idleTimeRange = new Vector2(3f, 10f);
    private float idleTime = 0;

    public EnemyStunST(EnemySCR instance, Transform playerTran) : base(instance, playerTran)
    {

    }

    public override void OnStateEnter()
    {
        _Instance._Agent.isStopped = true;
        idleTime = 3.5f;
        _time = 0;
        Debug.Log("AGHH!");
    }

    public override void OnStateExit()
    {
        _time = -1;
        idleTime = 0;
    }

    public override void OnStateUpdate()
    {
        if (_time >= 0)
        {
            _time += Time.deltaTime;
            if (_time >= idleTime)
            {
                //
                _Instance.SetState(new EnemyWanderST(_Instance, _PlayerTran));

            }
        }
    }
}
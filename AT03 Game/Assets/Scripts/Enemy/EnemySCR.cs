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
    [SerializeField] private GameObject privatePlayerTran;
    public GameObject playerGameObject { get; private set; }
    private Transform playerTran;
    public NavMeshAgent _Agent { get; private set; }

    private Outline outlineObject;
    private bool isStunned;
    private SphereCollider stunCol;

    //public Animator animator { get; private set; }
    public AudioSource audioSource { get; private set; }


    public EnemyIdleST enemyIdle;
    public EnemyChaseST enemyChase;
    public EnemyWanderST enemyWander;
    public EnemyStunST enemyStun;


    // Start is called before the first frame update

    protected override void Awake()
    {
        playerGameObject = privatePlayerTran;
        playerTran = playerGameObject.transform;


        enemyIdle = new EnemyIdleST(this,playerTran,enemyIdle);
        enemyChase = new EnemyChaseST(this, playerTran,enemyChase);
        enemyWander = new EnemyWanderST(this, playerTran,enemyWander);
        enemyStun = new EnemyStunST(this, playerTran);

        entryState = enemyIdle;

        //mainCurState = entryState;
        if (TryGetComponent(out NavMeshAgent agent) == true)
        {
            _Agent = agent;
        }

        if(TryGetComponent(out AudioSource aSrc) == true)
        {
            audioSource = aSrc;
        }
        if (TryGetComponent(out SphereCollider sphCol) == true)
        {
            stunCol = sphCol;
            stunCol.enabled = true;
        }

        //if (transform.GetChild(0).TryGetComponent(out Animator anim) == true)
        //{
        //    animator = anim;
        //}

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

        if ((Input.GetButtonDown("Use")) && (outlineObject.enabled == true))
        {
            //Debug.Log("Stunned");
            SetState(new EnemyStunST(this,playerTran));
            isStunned = true;
            stunCol.enabled = false;
            StartCoroutine(StunTimer());
        }
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(4);
        isStunned = false;
        stunCol.enabled = true;
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

[System.Serializable]
public class EnemyIdleST : EnemyBHST
{
    [SerializeField]
    private Vector2 idleTimeRange = new Vector2(3f, 10f);

    //ONCE THIS IS FIXED, DO THIS TO THE REST
    [SerializeField]
    private AudioClip idleSoundClip;

    private float _time = -1;
    private float idleTime = 0;


    public EnemyIdleST(EnemySCR instance, Transform playerTran, EnemyIdleST idle) : base(instance, playerTran)
    {
        idleTimeRange = idle.idleTimeRange;
        idleSoundClip = idle.idleSoundClip;
    }
    
    public override void OnStateEnter()
    {
        //Debug.Log("start Idle");
        _Instance._Agent.isStopped = true;
        idleTime = Random.Range(idleTimeRange.x,idleTimeRange.y);
        _time = 0;
        //Debug.Log(idleTime);
        //_Instance.animator.SetBool("IsMoving", false);


        _Instance.audioSource.PlayOneShot(idleSoundClip);

    }
    public override void OnStateUpdate()
    {
        if (Vector3.Distance(_Instance.transform.position, _PlayerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(_Instance.enemyChase);
        }

        if (_time >= 0)
        {
            _time += Time.deltaTime;
            if (_time >= idleTime)
            {
                //
                _Instance.SetState(_Instance.enemyWander);

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
[System.Serializable]
public class EnemyWanderST : EnemyBHST
{
    [SerializeField] 
    private float wanderSpeed = 7f;

    [SerializeField]
    private AudioClip wanderSoundClip;

    private Vector3 targetPOS;
    private bool foundPos = false;
    public EnemyWanderST(EnemySCR instance, Transform playerTran, EnemyWanderST wander) : base(instance, playerTran)
    {
        wanderSpeed = wander.wanderSpeed;
        wanderSoundClip = wander.wanderSoundClip;
    }

    public override void OnStateEnter()
    {
        _Instance._Agent.speed = wanderSpeed;
        
        //Debug.Log("wander start");
        //
        _Instance._Agent.isStopped = false;

        //_Instance.animator.SetBool("IsChasing", false);
        //_Instance.animator.SetBool("IsMoving", true);

        _Instance.audioSource.PlayOneShot(wanderSoundClip);
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
            //Debug.Log(targetPOS);

            NavMeshHit hitMesh;
            if (NavMesh.SamplePosition(targetPOS, out hitMesh, 55f, NavMesh.AllAreas))
            {
                _Instance._Agent.SetDestination(hitMesh.position);
                targetPOS = hitMesh.position;
                foundPos = true;
            }
        }

        if (Vector3.Distance(_Instance.transform.position, _PlayerTran.position) <= _Instance.viewRadius)
        {
            _Instance.SetState(_Instance.enemyChase);
        }
 
        if (Vector3.Distance(new Vector3(targetPOS.x, _Instance.transform.position.y, targetPOS.z), _Instance.transform.position) <= _Instance._Agent.stoppingDistance)
        {
            //Debug.Log("AHOY IDLE");
            _Instance.SetState(_Instance.enemyIdle);
        }
    }
}

[System.Serializable]
public class EnemyChaseST : EnemyBHST
{
    [SerializeField]
    private float chaseSpeed = 13f;

    [SerializeField]
    private AudioClip chaseSoundClip;

    public EnemyChaseST(EnemySCR instance, Transform playerTran, EnemyChaseST chase) : base(instance, playerTran)
    {
        chaseSpeed = chase.chaseSpeed;
        chaseSoundClip = chase.chaseSoundClip;
    }

    public override void OnStateEnter()
    {
        _Instance._Agent.speed = chaseSpeed;
        _Instance._Agent.isStopped = false;

        //_Instance.animator.SetBool("IsMoving", false);
        //_Instance.animator.SetBool("IsChasing", true);
        //Debug.Log("AHOY!");

        _Instance.audioSource.PlayOneShot(chaseSoundClip);
    }

    public override void OnStateExit()
    {
        //Debug.Log("boo.");
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
            _Instance.SetState(_Instance.enemyIdle);
        }

        _Instance._Agent.SetDestination(_PlayerTran.position);
    }
}


public class EnemyStunST : EnemyBHST
{
    private float _time = -1;
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
                _Instance.SetState(_Instance.enemyIdle);

            }
        }
    }
}
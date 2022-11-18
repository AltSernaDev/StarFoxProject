using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossManager: MonoBehaviour
{
    public static BossManager instance;


    [SerializeField] private float health = 1000;
    Animator animator;  

    public enum State
    {
        idle,
        moving,
        missile1,
        missile2,
        missile3,
        blackHole2,
        blackHole3,
        dead
    }
    public State state = State.idle;
    [SerializeField] bool isActionReady = false;
    public int phase;

    public float Health { get => health; }

    [Header("Missile")]
    [SerializeField] Transform missileSpawn;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float timeM, speedM, damageM;
    MissileS missileCode;

    [Header("BlackHole")]
    [SerializeField] GameObject blackHolePrefab;
    [SerializeField] float timeB, damageB;
    BlackHoleS blackHoleCode;

    Transform player_;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isActionReady)
        {
            isActionReady = false;
            //change state
            ActionStateMachine(PhacesStateMachine(health));
        }
    }

    public void TakeDamage(float damage)
    {
        if (health - damage >= 0)
            health -= damage;
        else
            health = 0;
    }

    State PhacesStateMachine(float health_)
    {
        State state_ = State.idle;
        switch (health_)
        {
            case float i when health_ > 600:
                phase = 1;
                //misiles1 
                state_ = State.missile1;
                //state_ = State.moving;
                break;
            case float i when health_ > 300:
                phase = 2;
                //misiles2 y BlackHole2
                state_ = State.blackHole2;
                break;
            case float i when health_ > 0:
                phase = 3;
                //misiles3 y BlackHole3
                state_ = State.blackHole3;
                break;
            case float i when health_ <= 0:
                phase = 0;
                //muerto 
                state_ = State.dead;
                break;
        }
        return state_;
    }
    void ActionStateMachine(State state_)
    {
        state = state_;
        switch (state_)
        {
            case State.idle:   
                StartCoroutine(IdleCoroutine());
                break;
            case State.moving:
                StartCoroutine(MovingCoroutine());
                break;                
            case State.missile1:
                StartCoroutine(Missile1Coroutine());
                break;
            case State.missile2:
                StartCoroutine(Missile2Coroutine());
                break;
            case State.missile3:
                StartCoroutine(Missile3Coroutine());
                break;
            case State.blackHole2:
                StartCoroutine(BlackHole2());
                break;
            case State.blackHole3:
                StartCoroutine(BlackHole3());
                break;
            case State.dead:
                StartCoroutine(DeadCoroutine());
                break;
        }
    }
   
    IEnumerator IdleCoroutine()
    {
        float timing = 12;
        animator.SetInteger("states", 0); //jales pikos
        yield return new WaitForSeconds(timing / ((phase + 1) / 2)); 
        isActionReady = true;
    }
    IEnumerator MovingCoroutine()
    {
        float timeLeft = 1;
        Vector3 target = player_.position;
        Vector3 targetF = player_.forward;
        Vector3 startSpeed = ((target - (targetF * 5)) - transform.position).normalized * -10;

        animator.SetFloat("speedMultiply", 0.4f);
        animator.SetInteger("states", 3);        

        yield return new WaitForSeconds(3.6f);//charge

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 4);

        while (timeLeft > 0) 
        {
            transform.position = Vector3.SmoothDamp(transform.position, target - (targetF * 20) + (new Vector3(0, -15, 0)), ref startSpeed, timeLeft);
            timeLeft -= Time.deltaTime; 

            yield return null;
        }

        yield return null;

        transform.DOMove(transform.position + new Vector3(0, 30, 0), 3);
        yield return new WaitForSeconds(3);

        yield return null;

        transform.DOMove(/*transform.parent.position*/ new Vector3(0, -4, 25), 3);
        yield return new WaitForSeconds(3);

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 0);

        ActionStateMachine(State.idle);
    }
    IEnumerator Missile1Coroutine()
    {
        animator.SetFloat("speedMultiply", 1.8f);
        animator.SetInteger("states", 1);

        yield return new WaitForSeconds(1.8f); //charge

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 2);

        yield return new WaitForSeconds(0.5f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM, damageM);

        yield return new WaitForSeconds(1.2f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM, damageM);

        yield return new WaitForSeconds(1.8f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM * 1.5f);

        yield return new WaitForSeconds(timeM);

        ActionStateMachine(State.moving);
    }
    IEnumerator Missile2Coroutine()
    {
        animator.SetFloat("speedMultiply", 1.89f);
        animator.SetInteger("states", 1);

        yield return new WaitForSeconds(1.5f); //charge

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 2);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(1f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(1f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(1.8f);
        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2f, damageM * 2f);

        yield return new WaitForSeconds(timeM);

        ActionStateMachine(State.moving);
    }
    IEnumerator Missile3Coroutine()
    {
        animator.SetFloat("speedMultiply", 1.89f);
        animator.SetInteger("states", 1);

        yield return new WaitForSeconds(1.5f); //charge

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 2);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);

        yield return new WaitForSeconds(1.5f); //2

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);

        yield return new WaitForSeconds(1.5f); //3

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, missileSpawn).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);

        yield return new WaitForSeconds(timeM);

        ActionStateMachine(State.moving);
    }
    IEnumerator BlackHole2()
    {
        animator.SetFloat("speedMultiply", 1.5f);
        animator.SetInteger("states", 5);

        yield return new WaitForSeconds(2f); //charge

        animator.SetFloat("speedMultiply", 1.42f);
        animator.SetInteger("states", 6);

        yield return new WaitForSeconds(1.2f);

        blackHoleCode = Instantiate(blackHolePrefab, (player_.position + player_.forward * 1.5f), Quaternion.Euler(Vector3.zero), gameObject.transform/*baseTransform*/).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(0.8f);

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 7);

        yield return new WaitForSeconds(1);

        ActionStateMachine(State.missile2);
    }
    IEnumerator BlackHole3()
    {
        animator.SetFloat("speedMultiply", 1.5f);
        animator.SetInteger("states", 5);

        yield return new WaitForSeconds(2f); //charge

        animator.SetFloat("speedMultiply", 1.42f);
        animator.SetInteger("states", 6);

        yield return new WaitForSeconds(1.2f);

        blackHoleCode = Instantiate(blackHolePrefab, (player_.position + player_.forward * 1.5f), Quaternion.Euler(Vector3.zero), gameObject.transform/*baseTransform*/).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(2f);

        blackHoleCode = Instantiate(blackHolePrefab, (player_.position + player_.forward * 1.5f), Quaternion.Euler(Vector3.zero), gameObject.transform/*baseTransform*/).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(2f);

        blackHoleCode = Instantiate(blackHolePrefab, (player_.position + player_.forward * 1.5f), Quaternion.Euler(Vector3.zero), gameObject.transform/*baseTransform*/).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(0.8f);

        animator.SetFloat("speedMultiply", 1f);
        animator.SetInteger("states", 7);

        yield return new WaitForSeconds(1);

        ActionStateMachine(State.missile3);
    }
    IEnumerator DeadCoroutine()
    {
        //dead anim
        yield return new WaitUntil(() => true == true /* /deadAnim/ .isPlaying == false*/);
        print("Man I'm dead");
        //endGame event
    }
}

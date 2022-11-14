using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager: MonoBehaviour
{
    public static BossManager instance;

    [SerializeField] private float health = 1000;
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
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float timeM, speedM, damageM;
    MissileS missileCode;

    [Header("BlackHole")]
    [SerializeField] GameObject blackHolePrefab;
    [SerializeField] float timeB, damageB;
    BlackHoleS blackHoleCode;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
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
        float timing = 7;
        yield return new WaitForSeconds(timing / ((phase + 1) / 2)); 
        isActionReady = true;
    }
    IEnumerator MovingCoroutine()
    {
        float timeLeft = 2;
        while (timeLeft > 0) // revisar distancia 
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime); // mover tempo
            timeLeft -= Time.deltaTime; 
            yield return null;
        }
        ActionStateMachine(State.idle);
    }
    IEnumerator Missile1Coroutine()
    {
        yield return new WaitForSeconds(1.8f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM, damageM);

        yield return new WaitForSeconds(1.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM, damageM);

        yield return new WaitForSeconds(1.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM * 1.5f);

        ActionStateMachine(State.moving);
    }
    IEnumerator Missile2Coroutine()
    {
        yield return new WaitForSeconds(1.5f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(0.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(0.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.2f, damageM);

        yield return new WaitForSeconds(1.8f);
        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2f, damageM * 2f);

        ActionStateMachine(State.moving);
    }
    IEnumerator Missile3Coroutine()
    {
        yield return new WaitForSeconds(1.5f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.5f, damageM);

        yield return new WaitForSeconds(1f); //2

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 1.8f, damageM);

        yield return new WaitForSeconds(0.6f); //3

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(timeM, speedM * 2.2f, damageM);
        yield return new WaitForSeconds(0.15f);

        ActionStateMachine(State.moving);
    }
    IEnumerator BlackHole2()
    {
        yield return new WaitForSeconds(1.8f); //charge

        blackHoleCode = Instantiate(blackHolePrefab, (GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 1.5f), Quaternion.Euler(Vector3.zero)).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(5f);

        ActionStateMachine(State.missile2);
    }
    IEnumerator BlackHole3()
    {
        yield return new WaitForSeconds(1.8f); //charge

        blackHoleCode = Instantiate(blackHolePrefab, (GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 1.5f), Quaternion.Euler(Vector3.zero)).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(1f);

        blackHoleCode = Instantiate(blackHolePrefab, (GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 1.5f), Quaternion.Euler(Vector3.zero)).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(1f);

        blackHoleCode = Instantiate(blackHolePrefab, (GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 1.5f), Quaternion.Euler(Vector3.zero)).GetComponent<BlackHoleS>();
        blackHoleCode.SetValues(timeB, damageB);

        yield return new WaitForSeconds(3f);

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

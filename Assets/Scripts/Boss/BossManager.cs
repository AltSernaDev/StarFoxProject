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

    public float Health { get => health; }


    [Header("Missile")]
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float time, speed, damage;
    MissileS missileCode;

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
                //misiles1 
                state_ = State.missile1;
                break;
            case float i when health_ > 300:
                //misiles2 y BlackHole2
                state_ = State.missile2;
                break;
            case float i when health_ > 0:
                //misiles3 y BlackHole3
                state_ = State.missile3;
                break;
            case float i when health_ <= 0:
                //muerto 
                
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
                break;
            case State.moving:                
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
                break;
            case State.blackHole3:
                break;
            case State.dead:
                StartCoroutine(DeadCoroutine());
                break;
        }
    }

    IEnumerator IdleCorrutine(float time_)//CHECK THIS
    {
        yield return new WaitForSeconds(time_); 
        isActionReady = true;
    }
    IEnumerator Missile1Coroutine()
    {
        yield return new WaitForSeconds(1.8f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed, damage);

        yield return new WaitForSeconds(1.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed, damage);

        yield return new WaitForSeconds(1.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.5f, damage * 1.5f);

        ActionStateMachine(State.idle);
    }
    IEnumerator Missile2Coroutine()
    {
        yield return new WaitForSeconds(1.5f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.2f, damage);

        yield return new WaitForSeconds(0.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.2f, damage);

        yield return new WaitForSeconds(0.8f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.2f, damage);

        yield return new WaitForSeconds(1.8f);
        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2f, damage * 2f);

        ActionStateMachine(State.idle);
    }
    IEnumerator Missile3Coroutine()
    {
        yield return new WaitForSeconds(1.5f); //charge

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.5f, damage);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.5f, damage);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.5f, damage);

        yield return new WaitForSeconds(1f); //2

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.8f, damage);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.8f, damage);
        yield return new WaitForSeconds(0.2f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 1.8f, damage);

        yield return new WaitForSeconds(0.6f); //3

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2.2f, damage);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2.2f, damage);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2.2f, damage);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2.2f, damage);
        yield return new WaitForSeconds(0.15f);

        missileCode = Instantiate(missilePrefab, transform).GetComponent<MissileS>();
        missileCode.SetValues(time, speed * 2.2f, damage);
        yield return new WaitForSeconds(0.15f);

        ActionStateMachine(State.idle);
    }
    IEnumerator DeadCoroutine()
    {
        //dead anim
        yield return new WaitUntil(() => true == true /* /deadAnim/ .isPlaying == false*/);
        print("Man I'm dead");
        //endGame event
    }
}

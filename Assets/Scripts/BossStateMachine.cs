using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    float health = 1000;
    public enum State
    {
        idle,
        stun,
        melee,
        missil,
        pem,
        blackHole,
        dead
    }
    public State state = State.idle;

    private void Update()
    {

    }

    State PhacesStateMachine(float health)
    {
        state = State.idle;
        switch (health)
        {
            case float i when health > 600:
                //misiles 
                break;
            case float i when health > 300:
                //misiles y pem
                break;
            case float i when health > 0:
                //misiles, pem y agujeros 
                break;
            case float i when health <= 0:
                //muerto 
                break;
        }
        return state;
    }
    void ActionStateMachine()
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.stun:
                break;
            case State.melee:
                break;
            case State.missil:
                break;
            case State.pem:
                break;
            case State.blackHole:
                break;
            case State.dead:
                break;
        }
    }
    void Missile()
    {

    }
    void PEM()
    {

    }
    void BlackHole()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileS : MonoBehaviour
{
    bool exploted;
    private float time = 5, speed = 1, damage = 10;
    Transform target;

    ParticleSystem particleSystem;

    public float Timee { get => time; }
    public float Speed { get => speed; }
    public float Damage { get => damage; }

    Vector3 acceleration;
    Vector3 velocity;

    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        target = GameObject.FindWithTag("Player").transform;
        Invoke("autoDestroy", time);
    }

    private void Update()
    {
        if (!exploted)
        {
            acceleration = (target.position - transform.position).normalized * speed;
            velocity += acceleration * Time.deltaTime;

            LookAt(transform.position + velocity);
            Vector3 finalPosition = velocity;
            transform.position += finalPosition * Time.deltaTime;
        }
    }
    void LookAt(Vector3 targetPosition)
    {
        Vector3 thisPos = transform.position;
        Vector3 forward = targetPosition - thisPos;

        transform.forward = forward.normalized;
    }

    public void SetValues(float time_, float speed_, float damage_)
    {
        time = time_;
        speed = speed_;
        damage = damage_;
    }
    void autoDestroy()
    {
        if (!exploted)
            StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        exploted = true;
        transform.GetChild(0).gameObject.SetActive(false);
        particleSystem.Play();
        yield return new WaitUntil(() => particleSystem.isPlaying == false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        else;
            //collision.gameObject.GetComponent </ PlayerHelthCode /> ().takeDamage(damage);

        autoDestroy();
    }

}

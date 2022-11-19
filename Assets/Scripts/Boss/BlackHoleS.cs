using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleS : MonoBehaviour
{
    private float time = 5, damage = 5;
    bool catch_;

    //Rigidbody pointerRB;
    GameObject player;
    GameObject pointer;
    PointerMove pointerCode;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        pointerCode = pointer.GetComponent<PointerMove>();
        gameObject.GetComponent<Collider>().enabled = false;

        Invoke("AutoDestroy", time);
        Invoke("EnableTrigger", 1);
    }

    public void SetValues(float time_, float damage_)
    {
        time = time_;
        damage = damage_;
    }

    void EnableTrigger()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }
    void AutoDestroy()
    {
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        yield return null; //temp
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pointer"))
        {
            player.GetComponent<Health>().TakeDamage(damage * Time.deltaTime);
            pointerCode.modifier = (transform.position - pointer.transform.position) * 0.327f;
        }
    }
}

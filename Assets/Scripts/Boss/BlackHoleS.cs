using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleS : MonoBehaviour
{
    private float time = 5, damage = 5;
    bool catch_;

    Rigidbody playerRB;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        Invoke("autoDestroy", time);
    }
    private void Update()
    {
        if (catch_)
        {
            playerRB.AddForce(transform.position - player.transform.position);
            //player.GetComponent </ PlayerHelthCode /> ().takeDamage(damage * Time.deltaTime);
        }
    }

    public void SetValues(float time_, float damage_)
    {
        time = time_;
        damage = damage_;
    }
    void autoDestroy()
    {
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        /*transform.GetChild(0).gameObject.SetActive(false);
        particleSystem.Play();
        yield return new WaitUntil(() => particleSystem.isPlaying == false);*/
        yield return null; //temp
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            catch_ = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            catch_ = false;
    }
}

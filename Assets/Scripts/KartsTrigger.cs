using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KartsTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent Trigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger.Invoke();
        }
    }
}

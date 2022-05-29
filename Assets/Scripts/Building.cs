using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class Building : MonoBehaviour
{
    [SerializeField] private UnityEvent _intruded;
    [SerializeField] private UnityEvent _gone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
            _intruded.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
            _gone.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class Building : MonoBehaviour
{
    public UnityAction _statusChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
            _statusChanged.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
            _statusChanged.Invoke();
    }
}
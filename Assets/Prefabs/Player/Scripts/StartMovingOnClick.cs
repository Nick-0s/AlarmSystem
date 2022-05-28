using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StartMovingOnClick : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private AlarmSystem _alarm;
    private bool _isMoveStarted;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _alarm = FindObjectOfType<AlarmSystem>();
        _isMoveStarted = false;
    }

    private void OnMouseDown()
    {
        _animator.SetTrigger("StartMoving");
        _isMoveStarted = true;
    }

    private void Update()
    {
        if(_isMoveStarted)
            Move();
    }

    private void Move()
    {
        float currentSpeed;

        if (_alarm.IsTriggered == false)
        {
            currentSpeed = _walkSpeed;
            _animator.SetBool("Alarmed", false);
        }
        else
        {
            currentSpeed = _runSpeed;
            _animator.SetBool("Alarmed", true);
        }

        transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
    }
}

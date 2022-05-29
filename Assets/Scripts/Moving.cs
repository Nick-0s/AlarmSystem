using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Moving : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private Alarm _alarm;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _alarm = FindObjectOfType<Alarm>();
    }

    private void Update()
    {
        float currentSpeed = 0;

        if(Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            currentSpeed = _alarm.IsTriggered? _runSpeed : _walkSpeed;
            transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            currentSpeed = _alarm.IsTriggered? _runSpeed : _walkSpeed;
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
        }

        _animator.SetFloat(AnimatorPlayerController.Params.Speed, currentSpeed);
    }
}
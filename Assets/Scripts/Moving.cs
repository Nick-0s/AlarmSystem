using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Moving : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float currentSpeed = 0;

        if(Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            currentSpeed = _alarm.IsTriggered? _runSpeed : _walkSpeed;
            transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            currentSpeed = _alarm.IsTriggered? _runSpeed : _walkSpeed;
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
        }

        _animator.SetFloat(AnimatorPlayerController.Params.Speed, currentSpeed);
    }
}
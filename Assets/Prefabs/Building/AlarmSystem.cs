using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] private AudioSource _alarmSound;

    public bool IsTriggered { get; private set; }

    public void TurnOff()
    {
        IsTriggered = false;
    }

    private void OnValidate()
    {
        if(_minVolume < 0)
            _minVolume = 0;

        if(_minVolume > 1)
            _minVolume = 1;

        if (_volumeChangeRate <= 0)
            _volumeChangeRate = 0.01f;
    }

    private void Awake()
    {
        IsTriggered = false;
        _alarmSound.volume = _minVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
        {
            IsTriggered = true;
            _alarmSound.Play();
        }
    }

    private void Update()
    {
        if(IsTriggered)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, 1, _volumeChangeRate * Time.deltaTime);
        }
        else
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _minVolume, _volumeChangeRate * Time.deltaTime);

            if (_alarmSound.volume == _minVolume)
                _alarmSound.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] private AudioSource _alarmSound;

    private Coroutine _alarm;
    private bool _isVolumeIncreasing;

    public bool IsTriggered { get; private set; }    

    private void OnValidate()
    {
        _minVolume = Mathf.Clamp(_minVolume, 0, 1);
        _volumeChangeRate = Mathf.Clamp(_volumeChangeRate, 0.01f, 1);
    }

    private void Awake()
    {
        IsTriggered = false;
        _alarmSound.volume = _minVolume;
        _isVolumeIncreasing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
        {
            IsTriggered = true;
            _alarm = StartCoroutine(SoundAlarm());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Robber>(out Robber robber))
        {
            IsTriggered = false;
        }
    }

    private IEnumerator SoundAlarm()
    {
        _alarmSound.Play();

        while (IsTriggered)
        {
            ChangeVolume();

            yield return null;
        }

        _alarmSound.Stop();
    }

    private void ChangeVolume()
    {
        float targetVolume = _isVolumeIncreasing? 1 : _minVolume;

        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangeRate * Time.deltaTime);
        
        if(_alarmSound.volume == targetVolume)
            _isVolumeIncreasing = _isVolumeIncreasing? false : true;
    }
}
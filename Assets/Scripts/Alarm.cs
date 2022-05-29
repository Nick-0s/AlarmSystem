using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] private AudioSource _alarmSound;

    private Coroutine _changingVolume;

    public bool IsTriggered { get; private set; }    

    public void TurnOn()
    {
        float maxVolume = 1;        
        IsTriggered = true;
        _alarmSound.Play();

        RemoveActiveCoroutine();

        _changingVolume = StartCoroutine(ChangeVolumeTo(maxVolume));
    }

    public void TurnOff()
    {
        IsTriggered = false;

        RemoveActiveCoroutine();

        _changingVolume = StartCoroutine(ChangeVolumeTo(_minVolume));
    }

    private void OnValidate()
    {
        _minVolume = Mathf.Clamp(_minVolume, 0, 1);
        _volumeChangeRate = Mathf.Clamp(_volumeChangeRate, 0.01f, 1);
    }

    private void Awake()
    {
        IsTriggered = false;
        _alarmSound.volume = _minVolume;
    }    

    private IEnumerator ChangeVolumeTo(float targetVolume)
    {
        while (_alarmSound.volume != targetVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangeRate * Time.deltaTime);

            yield return null;
        }

        if (_alarmSound.volume == _minVolume)
            _alarmSound.Stop();
    }

    private void RemoveActiveCoroutine()
    {
        if(_changingVolume != null)
            StopCoroutine(_changingVolume);
    }
}
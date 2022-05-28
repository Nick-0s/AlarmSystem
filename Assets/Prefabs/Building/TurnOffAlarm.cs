using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAlarm : MonoBehaviour
{
    private AlarmSystem _alarm;

    private void Awake()
    {
        _alarm = GetComponentInChildren<AlarmSystem>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _alarm.TurnOff();
    }
}
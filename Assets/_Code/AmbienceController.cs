using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AmbienceController : MonoBehaviour
{
    
    private EventInstance outsideSnapshot;
    private EventInstance insideSnapshot;
    private EventInstance daySnapshot;
    private EventInstance sunsetSnapshot;
    private EventInstance nightSnapshot;

    private void Start()
    {
        outsideSnapshot =  RuntimeManager.CreateInstance("snapshot:/Outside");
        insideSnapshot =  RuntimeManager.CreateInstance("snapshot:/Inside");
        daySnapshot = RuntimeManager.CreateInstance("snapshot:/Day");
        sunsetSnapshot = RuntimeManager.CreateInstance("snapshot:/Sunset");
        nightSnapshot = RuntimeManager.CreateInstance("snapshot:/Night");
        
        DayNightController.TimeOfDayChangedEvent += OnTimeOfDayChanged;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideSnapshot.start();
            outsideSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
            //outsideSnapshot.start();
            //insideSnapshot.release();            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
            outsideSnapshot.start();
        }
    }

    void OnTimeOfDayChanged(TimeOfDay timeOfDay)
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Day:
                daySnapshot.start();
                sunsetSnapshot.stop(STOP_MODE.IMMEDIATE);
                nightSnapshot.stop(STOP_MODE.IMMEDIATE);
                break;
            case TimeOfDay.Sunset:
                daySnapshot.stop(STOP_MODE.IMMEDIATE);
                sunsetSnapshot.start();
                nightSnapshot.stop(STOP_MODE.IMMEDIATE);
                break;
            case TimeOfDay.Night:
                daySnapshot.stop(STOP_MODE.IMMEDIATE);
                sunsetSnapshot.stop(STOP_MODE.IMMEDIATE);
                nightSnapshot.start();
                break;
        }
    }
}

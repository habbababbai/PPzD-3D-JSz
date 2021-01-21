using System;
using UnityEngine;


public class DayNightController : MonoBehaviour {
    public static event Action<TimeOfDay> TimeOfDayChangedEvent = delegate { };

    TimeOfDay timeOfDay;
    

    public TimeOfDay TimeOfDay {
        get => timeOfDay;
        set {
            if (timeOfDay != value) {
                TimeOfDayChangedEvent(value);
                timeOfDay = value;
            }
        }
    }

    public void GotoNextStage() {
        TimeOfDay = (TimeOfDay) (((int) timeOfDay + 1) % 3);
        Debug.Log("Time of day is: " + timeOfDay);
    }
}
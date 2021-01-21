using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

#pragma warning disable CS0649

public class TimeyWimeyRenderer : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite daySprite;
    [SerializeField] Sprite sunsetSprite;
    [SerializeField] Sprite nightSprite;
    

    void Awake() {
        DayNightController.TimeOfDayChangedEvent += OnTimeOfDayChanged;
    }

    void OnDestroy() {
        DayNightController.TimeOfDayChangedEvent -= OnTimeOfDayChanged;
    }

    void OnTimeOfDayChanged(TimeOfDay timeOfDay) {
        switch (timeOfDay) {
            case TimeOfDay.Day:
                spriteRenderer.sprite = daySprite;
                break;
            case TimeOfDay.Sunset:
                spriteRenderer.sprite = sunsetSprite;
                break;
            case TimeOfDay.Night:
                spriteRenderer.sprite = nightSprite;
                break;
        }
    }
}
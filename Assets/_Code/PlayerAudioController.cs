using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem
{

    [SerializeField, EventRef] private string jumpEvent;
    [SerializeField, EventRef] private string landingEvent;
    [SerializeField, EventRef] private string deathEvent;
    [SerializeField, EventRef] private string attackEvent;
    [SerializeField, EventRef] private string footstepEvent;

    public override void HandleEvent(PlayerEventType eventType) {
        switch (eventType) {
            case PlayerEventType.Jump:
                RuntimeManager.PlayOneShot(jumpEvent);
                break;
            case PlayerEventType.Landing:
                RuntimeManager.PlayOneShot(landingEvent);
                break;
            case PlayerEventType.Death:
                RuntimeManager.PlayOneShot(deathEvent);
                break;
            case PlayerEventType.Attack:
                RuntimeManager.PlayOneShot(attackEvent);
                break;
            case PlayerEventType.Footstep:
                RuntimeManager.PlayOneShot(footstepEvent);
                break;
        }
    }
}

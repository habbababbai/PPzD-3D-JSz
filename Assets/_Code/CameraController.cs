#pragma warning disable CS0649
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Transform target;
    float followSpeed = 2f;


    void Update() {
        if (!target)
            return;
        var newPosition = Vector2.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.position = newPosition;
    }
}
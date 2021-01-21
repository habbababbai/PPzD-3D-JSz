using UnityEngine;

public class Killzone : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //todo move player to start
        }
    }
}
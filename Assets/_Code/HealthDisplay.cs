using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    [SerializeField] Image fill;

    public void SetPercentage(float value) {
        fill.fillAmount = value;
    }
}
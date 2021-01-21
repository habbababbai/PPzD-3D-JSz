using UnityEngine;

public static class Input {
    public static bool IsLocked { get; set; }

    public static bool GetKeyDown(KeyCode key, bool ignoreLock = false) {
        if (!ignoreLock && IsLocked)
            return false;
        return UnityEngine.Input.GetKeyDown(key);
    }

    public static bool GetKeyDown(string keyName) {
        return !IsLocked && UnityEngine.Input.GetKeyDown(keyName);
    }

    public static float GetAxis(string axisName) {
        return IsLocked ? 0 : UnityEngine.Input.GetAxis(axisName);
    }

    public static bool GetMouseButtonDown(int button) {
        return !IsLocked && UnityEngine.Input.GetMouseButtonDown(button);
    }
}